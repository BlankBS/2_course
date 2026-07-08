# Лабораторная работа 7 — ASP.NET Core Web Application (ASPA007)

## Что это

Веб-приложение **"Celebrities Dictionary Internet Service"** — справочник известных учёных и деятелей IT.  
Реализовано на **ASP.NET Core 8** с двумя слоями взаимодействия:

- **REST API** (Minimal API) — для программного доступа к данным
- **Razor Pages** — для просмотра и добавления знаменитостей через браузер

Использует **Entity Framework Core** + **MS SQL Server LocalDB** для хранения данных.  
DAL (Data Access Layer) переиспользуется из лаб. 6 (`DAL_Celebrity_MSSQL`).

---

## Структура проекта

```
ASPA007_1/
├── Program.cs                    # точка входа, настройка DI и middleware
├── CelebrityAPI.cs               # Minimal API эндпоинты
├── CelebritiesAPIExtensions.cs   # расширения для настройки DI
├── CelebritiesConfig.cs          # POCO-конфиг (пути, строка подключения)
├── MiddlewareErrorHandler.cs     # кастомный обработчик ошибок
├── Celebrities.config.json       # конфиг-файл (фото-путь, conn string)
├── Pages/
│   ├── Celebrities.cshtml(.cs)   # страница — галерея фотографий
│   ├── Celebrity.cshtml(.cs)     # страница — карточка знаменитости
│   ├── NewCelebrity.cshtml(.cs)  # страница — добавление новой записи
│   └── Shared/
│       └── _CelebritiesLayout.cshtml  # общий layout
└── images/                       # фотографии знаменитостей
```

---

## Модели данных

### Celebrity (знаменитость)

| Поле          | Тип      | Описание                         |
|---------------|----------|----------------------------------|
| `Id`          | int      | первичный ключ                   |
| `FullName`    | string   | полное имя (до 50 символов)      |
| `Nationality` | string   | гражданство (2 символа, ISO 3166)|
| `ReqPhotoPath`| string?  | имя файла фото                   |

### Lifeevent (событие в жизни знаменитости)

| Поле           | Тип       | Описание                              |
|----------------|-----------|---------------------------------------|
| `id`           | int       | первичный ключ                        |
| `CelebrityId`  | int       | внешний ключ → Celebrity              |
| `Date`         | DateTime? | дата события                          |
| `Description`  | string    | описание события (до 256 символов)    |
| `ReqPhotoPath` | string?   | имя файла фото события                |

---

## REST API

### Знаменитости `/api/Celebrities`

| Метод    | URL                                | Описание                              |
|----------|------------------------------------|---------------------------------------|
| `GET`    | `/api/Celebrities`                 | все знаменитости                      |
| `GET`    | `/api/Celebrities/{id}`            | знаменитость по ID                    |
| `GET`    | `/api/Celebrities/Lifeevents/{id}` | событие по ID                         |
| `GET`    | `/api/Celebrities/photo/{fname}`   | фото по имени файла (bytes)           |
| `POST`   | `/api/Celebrities`                 | добавить знаменитость (JSON body)     |
| `PUT`    | `/api/Celebrities/{id}`            | обновить знаменитость (JSON body)     |
| `DELETE` | `/api/Celebrities/{id}`            | удалить знаменитость                  |

### События `/api/Lifeevents`

| Метод    | URL                                    | Описание                              |
|----------|----------------------------------------|---------------------------------------|
| `GET`    | `/api/Lifeevents`                      | все события                           |
| `GET`    | `/api/Lifeevents/{id}`                 | событие по ID                         |
| `GET`    | `/api/Lifeevents/Celebrities/{id}`     | все события знаменитости по её ID     |
| `POST`   | `/api/Lifeevents`                      | добавить событие (JSON body)          |
| `PUT`    | `/api/Lifeevents/{id}`                 | обновить событие (JSON body)          |
| `DELETE` | `/api/Lifeevents/{id}`                 | удалить событие                       |

### Фотографии

| Метод | URL              | Описание                         |
|-------|------------------|----------------------------------|
| `GET` | `/Photos/{fname}`| отдать файл фото (stream-режим)  |

> Разница между `/api/Celebrities/photo/{fname}` и `/Photos/{fname}`: первый читает файл в память целиком (`ReadAllBytesAsync`), второй стримит побуферно (`BinaryReader/BinaryWriter` по 2 Кб).

---

## Razor Pages (UI)

| URL                      | Страница          | Описание                                                   |
|--------------------------|-------------------|------------------------------------------------------------|
| `/`                      | `Celebrities`     | галерея фото всех знаменитостей, клик — переход на карточку|
| `/{id}` или `/Celebrities/{id}` | `Celebrity` | карточка конкретной знаменитости с фото               |
| `/0`                     | `NewCelebrity`    | форма добавления: имя + гражданство + загрузка фото       |

### Логика добавления новой знаменитости (`NewCelebrity`)

1. Пользователь вводит имя, гражданство, выбирает фото → `POST` без `press`
2. Файл сохраняется в папку `PhotoPath` (если имя занято — добавляется GUID)
3. Показывается страница подтверждения с превью фото
4. Пользователь нажимает **CONFIRMATION** → `POST` с `press=Confirm` → запись добавляется в БД → редирект на галерею

### Content Negotiation на странице Celebrity

Страница `Celebrity` читает заголовок `Accept` из запроса.  
Если клиент предпочитает `json` — делает редирект на API-эндпоинт.  
Если предпочитает `html` (или не указан) — рендерит Razor-страницу.

---

## Конфигурация

Файл `Celebrities.config.json`:

```json
{
  "Celebrities": {
    "PhotosRequestPath": "/Photos",
    "PhotoPath": "<абсолютный путь к папке с фото>",
    "ConnectionString": "Server=(localdb)\\MSSQLLocalDB;Database=LES01;Trusted_Connection=True;"
  }
}
```

- `PhotosRequestPath` — URL-префикс для запросов фото (используется в `<img src="...">`)
- `PhotoPath` — путь к папке на диске, откуда читаются/куда сохраняются файлы
- `ConnectionString` — строка подключения к MS SQL LocalDB

---

## База данных

- Движок: **MS SQL Server LocalDB**, база `LES01`
- При каждом запуске база **пересоздаётся** (`Init.Execute(delete: true, create: true)`)
- Таблицы: `Celebrities`, `Lifeevents` (создаются через EF Core `OnModelCreating`)
- Связь: `Lifeevents.CelebrityId` → `Celebrities.Id` (один-ко-многим)

---

## Обработка ошибок

`MiddlewareErrorHandler` регистрирует два маршрута-обработчика:

- `GET /Celebrities/Error` — возвращает `500 Problem` для ошибок раздела знаменитостей
- `GET /Lifeevents/Error` — аналогично для событий

В production-режиме дополнительно подключается стандартный `UseExceptionHandler("/Error")`.

---

## DI-контейнер (что регистрируется)

| Сервис               | Lifetime  | Описание                                                      |
|----------------------|-----------|---------------------------------------------------------------|
| `IRepository`        | Scoped    | репозиторий EF Core, создаётся с `ConnectionString` из конфига|
| `CelebrityTitles`    | Singleton | заголовки сайта (Head, Title, Copyright)                      |
| `CelebritiesConfig`  | Options   | конфиг из секции `Celebrities` в JSON                         |
| Razor Pages          | —         | стандартная регистрация страниц                               |

---

## Маршруты Razor Pages

```csharp
/Celebrities  →  /          (главная — галерея)
/NewCelebrity →  /0         (форма добавления)
/Celebrity    →  /Celebrities/{id:int:min(1)}
/Celebrity    →  /{id:int:min(1)}
```

---

## Зависимости (NuGet, из лаб. 6)

- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.Data.SqlClient`
- `Azure.Identity` (через транзитивную зависимость SqlClient)

---

## Быстрый запуск

1. Убедиться, что установлен MS SQL Server LocalDB
2. Скорректировать `PhotoPath` в `Celebrities.config.json`
3. Запустить проект — БД создастся автоматически
4. Открыть `https://localhost:<port>/` для UI или обращаться к `/api/Celebrities` для API
