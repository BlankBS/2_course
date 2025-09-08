#include <iostream>
#include <vector>
#include <string>
#include <algorithm>
#include <iomanip>

bool isMistake = false;

class Item {
private:
    std::string name;
    int cost;
    int weight;

public:
    Item(const std::string& n = "", int c = 0, int w = 0)
        : name(n), cost(c), weight(w) {
    }

    const std::string& getName() const { return name; }
    int getCost() const { return cost; }
    int getWeight() const { return weight; }

    void setName(const std::string& n) { name = n; }
    void setCost(int c) { cost = c; }
    void setWeight(int w) { weight = w; }

    void display() const {
        std::cout << std::setw(15) << name
            << std::setw(10) << cost
            << std::setw(10) << weight << std::endl;
    }
};

class KnapsackSolver {
private:
    int capacity;
    std::vector<Item> items;

public:
    KnapsackSolver(int cap = 0) : capacity(cap) {}

    void setCapacity(int cap) {
        capacity = cap;
    }

    void addItem(const Item& item) {
        items.push_back(item);
    }

    void inputItems() {
        int count;
        std::cout << "Введите количество предметов: ";
        std::cin >> count;
        if (count <= 0)
        {
            isMistake = true;
            std::cout << "\n\nКоличество предметов должно быть натуральным числом!\n\n";
            return;
        }

        for (int i = 0; i < count; ++i) {
            std::string name;
            int cost, weight;

            std::cout << "\nПредмет " << i + 1 << ":\n";
            std::cout << "Название: ";
            std::cin >> name;
            std::cout << "Стоимость: ";
            std::cin >> cost;
            if (cost <= 0)
            {
                isMistake = true;
                std::cout << "\n\nЦена должна быть положительным числом!\n\n";
                return;
            }
            std::cout << "Вес: ";
            std::cin >> weight;
            if (weight <= 0)
            {
                isMistake = true;
                std::cout << "\n\nВес должен быть положительным числом!\n\n";
                return;
            }

            items.emplace_back(name, cost, weight);
        }
    }

    void solve() {
        int n = items.size();
        if (n == 0 || capacity == 0) {
            isMistake = true;
            std::cout << "Нет предметов или вместимость равна 0!\n";
            return;
        }

        std::vector<std::vector<int>> dp(n + 1, std::vector<int>(capacity + 1, 0));

        for (int i = 1; i <= n; ++i) {
            for (int w = 1; w <= capacity; ++w) {
                int currentWeight = items[i - 1].getWeight();
                int currentCost = items[i - 1].getCost();

                if (currentWeight <= w) {
                    dp[i][w] = std::max(
                        dp[i - 1][w],
                        dp[i - 1][w - currentWeight] + currentCost
                    );
                }
                else {
                    dp[i][w] = dp[i - 1][w];
                }
            }
        }

        int maxCost = dp[n][capacity];
        int w = capacity;
        std::vector<Item> selectedItems;

        for (int i = n; i > 0 && maxCost > 0; --i) {
            if (maxCost != dp[i - 1][w]) {
                selectedItems.push_back(items[i - 1]);
                maxCost -= items[i - 1].getCost();
                w -= items[i - 1].getWeight();
            }
        }

        displayResults(selectedItems, dp[n][capacity]);
    }

    void displayResults(const std::vector<Item>& selectedItems, int totalCost) {
        std::cout << "\n" << std::string(50, '=') << "\n";
        std::cout << "РЕЗУЛЬТАТЫ РЕШЕНИЯ ЗАДАЧИ О РЮКЗАКЕ\n";
        std::cout << std::string(50, '=') << "\n";

        std::cout << "\nВсе предметы:\n";
        std::cout << std::setw(15) << "Название"
            << std::setw(10) << "Стоимость"
            << std::setw(10) << "Вес" << std::endl;
        std::cout << std::string(35, '-') << std::endl;

        for (const auto& item : items) {
            item.display();
        }

        std::cout << "\nВыбранные предметы:\n";
        std::cout << std::setw(15) << "Название"
            << std::setw(10) << "Стоимость"
            << std::setw(10) << "Вес" << std::endl;
        std::cout << std::string(35, '-') << std::endl;

        int totalWeight = 0;
        for (const auto& item : selectedItems) {
            item.display();
            totalWeight += item.getWeight();
        }

        std::cout << std::string(35, '-') << std::endl;
        std::cout << "Итого:"
            << std::setw(9) << ""
            << std::setw(10) << totalCost
            << std::setw(10) << totalWeight << std::endl;

        std::cout << "\nВместимость рюкзака: " << capacity << std::endl;
        std::cout << "Использовано веса: " << totalWeight << "/" << capacity << std::endl;
        std::cout << "Максимальная стоимость: " << totalCost << std::endl;
    }

    void clear() {
        items.clear();
        capacity = 0;
    }
};

int main() {
    setlocale(LC_CTYPE, "rus");

    KnapsackSolver solver;
    int choice;

    do {
        if (isMistake)
        {
            std::cout << "\n\nОшибка!\n\n";
            break;
        }
        std::cout << "\n" << std::string(50, '=') << "\n";
        std::cout << "РЕШЕНИЕ ЗАДАЧИ О РЮКЗАКЕ (БЕЗ ПОВТОРЕНИЙ)\n";
        std::cout << std::string(50, '=') << "\n";
        std::cout << "1. Ввести данные и решить задачу\n";
        std::cout << "2. Выход\n";
        std::cout << "Выберите действие: ";
        std::cin >> choice;

        switch (choice) {
        case 1: {
            if (isMistake)
            {
                std::cout << "\n\nОшибка!\n\n";
                break;
            }
            int capacity;
            std::cout << "Введите вместимость рюкзака: ";
            std::cin >> capacity;
            solver.setCapacity(capacity);

            solver.inputItems();
            solver.solve();
            solver.clear();
            break;
        }
        case 2:
            std::cout << "Выход из программы...\n";
            break;
        default:
            std::cout << "Неверный выбор!\n";
        }
    } while (choice != 2);

    return 0;
}