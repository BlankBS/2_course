#include <iostream>
#include <vector>
#include <sstream>
#include <algorithm>
#include <ctime>
#include <climits>

bool isMistake = false;

bool checkOnLetter()
{
	if (!std::cin)
	{
		std::cin.clear();
		std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
		return true;
	}
	return false;
}

void ValidatedIntInput(std::string str1, int& EnterVariable, std::string str2, int minValue = 0, int maxValue = 0)
{
	do
	{
		isMistake = false;
		std::cout << str1;
		std::cin >> EnterVariable;
		if (maxValue == 0)
		{
			maxValue = INT_MAX;
		}
		if (EnterVariable < minValue || EnterVariable > maxValue)
		{
			isMistake = true;
		}
		if (checkOnLetter())
		{
			isMistake = true;
		}
		if (isMistake)
		{
			std::cout << str2 << '\n';
		}
	} while (isMistake);
}

struct Individ
{
	std::vector<int> route;
	int routeSum;

	Individ(int v) : route(v), routeSum(INT_MAX) {}

	void calculateRouteSum(const std::vector<std::vector<int>>& adjMatrix)
	{
		routeSum = 0;
		for (int i = 0; i < route.size() - 1; i++)
		{
			int from = route[i] - 1;
			int to = route[i + 1] - 1;

			if (adjMatrix[from][to] == INT_MAX)
			{
				routeSum = INT_MAX;
				return;
			}
			routeSum += adjMatrix[from][to];
		}

		int last = route.back() - 1;
		int first = route[0] - 1;

		if (adjMatrix[last][first] == INT_MAX)
		{
			routeSum = INT_MAX;
			return;
		}
		routeSum += adjMatrix[last][first];
	}

	void generateRandomRoute(const std::vector<std::vector<int>>& adjMatrix)
	{
		for (int i = 0; i < route.size(); i++)
		{
			route[i] = i + 1;
		}
		std::random_shuffle(route.begin(), route.end());
		calculateRouteSum(adjMatrix);
	}

	void orderedCrossover(const Individ& parent1, const Individ& parent2, int mutationChance, const std::vector<std::vector<int>>& adjMatrix)
	{
		int n = parent1.route.size();
		route = std::vector<int>(n, 0);

		int point1 = rand() % n;
		int point2 = rand() % n;
		if (point1 > point2)
		{
			std::swap(point1, point2);
		}

		std::vector <bool> used(n + 1, false);

		for (int i = point1; i <= point2; i++)
		{
			route[i] = parent1.route[i];
			used[parent1.route[i]] = true;
		}

		int pos = 0;
		for (int i = 0; i < n; i++)
		{
			if (i >= point1 && i <= point2)
			{
				continue;
			}

			while (pos < n && used[parent2.route[pos]])
			{
				pos++;
			}

			if (pos < n)
			{
				route[i] = parent2.route[pos];
				used[parent2.route[pos]] = true;
				pos++;
			}
		}

		if ((rand() % 100) < mutationChance)
		{
			int idx1 = rand() % n;
			int idx2 = rand() % n;
			while (idx1 == idx2)
			{
				idx2 = rand() % n;
			}
			std::swap(route[idx1], route[idx2]);

			calculateRouteSum(adjMatrix);
		}
	}

	bool operator==(const Individ& other) const
	{
		return route == other.route;
	}
};

void sortPopulation(std::vector<Individ>& population)
{
	std::sort(population.begin(), population.end(), [](const Individ& a, const Individ& b)
		{
			return a.routeSum < b.routeSum;
		});
}

class Graph
{
public:
	int vertices;
	std::vector<std::vector<int>> adjMatrix;

public:
	Graph(int v) : vertices(v)
	{
		adjMatrix.resize(v, std::vector<int>(v, INT_MAX));
	}

	int getVerticesCount() const { return vertices; }
	const std::vector<std::vector<int>>& getAdjMatrix() const { return adjMatrix; }

	void addEdge(int from, int to, int weight)
	{
		if (from != to && weight >= 0)
		{
			adjMatrix[from - 1][to - 1] = weight;
		}
	}

	int getDistance(int from, int to) const
	{
		return adjMatrix[from - 1][to - 1];
	}

	void addCity()
	{
		vertices++;

		adjMatrix.push_back(std::vector<int>(vertices, INT_MAX));

		for (int i = 0; i < vertices - 1; i++)
		{
			adjMatrix[i].push_back(INT_MAX);
		}

		int newCity = vertices;

		for (int i = 1; i < vertices; i++)
		{
			int cost;

			std::ostringstream oss;
			oss << "Введите стоимость пути из города " << newCity << " в город " << i << " (>= 0): ";
			ValidatedIntInput(oss.str(), cost, "Стоимость должна быть натуральным числом. Повторите ввод\n");
			//std::cout << oss.str();
			addEdge(newCity, i, cost);

			std::ostringstream oss2;
			oss2 << "Введите стоимость пути из города " << i << " в город " << newCity << " (>= 0): ";
			ValidatedIntInput(oss2.str(), cost, "Стоимость должна быть неотрицательным числом.");
			addEdge(i, newCity, cost);
		}
		std::cout << "Город " << newCity << " добавлен. Всего городов: " << vertices << '\n';
	}

	void deleteCity(int city)
	{
		if (city < 1 || city > vertices)
		{
			std::cout << "Неверный номер города!\n";
			return;
		}

		int cityIndex = city - 1;
		adjMatrix.erase(adjMatrix.begin() + cityIndex);

		for (auto& row : adjMatrix)
		{
			row.erase(row.begin() + cityIndex);
		}

		vertices--;

		std::cout << "Город " << city << " удален.\n";
	}

	void printAdjMatrix() const
	{
		std::cout << "\nМатрица смежности графа:\n";
		std::cout << "   ";
		for (int i = 1; i <= vertices; i++)
		{
			std::cout << i << '\t';
		}
		std::cout << '\n';

		for (int i = 0; i < vertices; i++)
		{
			std::cout << i + 1 << ": ";
			for (int j = 0; j < vertices; j++)
			{
				if (adjMatrix[i][j] == INT_MAX)
				{
					std::cout << "INT\t";
				}
				else
				{
					std::cout << adjMatrix[i][j] << '\t';
				}
			}
			std::cout << '\n';
		}
	}

	bool isCompleteGraph() const
	{
		for (int i = 0; i < vertices; i++)
		{
			for (int j = 0; j < vertices; j++)
			{
				if (i != j && adjMatrix[i][j] == INT_MAX)
				{
					return false;
				}
			}
		}
		return true;
	}
};

void initializeGraph(Graph& graph)
{
	std::vector<std::vector<int>> costs =
	{
		{INT_MAX, 17,   10,   11,   30,   25,   18,   22},   
		{14,   INT_MAX, 19,   12,   17,   20,   15,   24}, 
		{15,   12,   INT_MAX, 17,   10,   22,   16,   19}, 
		{20,   18,   14,   INT_MAX, 12,   28,   21,   25}, 
		{25,   22,   19,   17,   INT_MAX, 30,   23,   27}, 
		{18,   16,   21,   15,   28,   INT_MAX, 19,   14}, 
		{22,   19,   13,   24,   20,   17,   INT_MAX, 16}, 
		{24,   21,   16,   19,   25,   14,   20,   INT_MAX}
	};

	for (int i = 1; i <= costs.size(); i++)
	{
		for (int j = 1; j <= costs[0].size(); j++)
		{
			if (i != j)
			{
				graph.addEdge(i, j, costs[i - 1][j - 1]);
			}
		}
	}
}

int main()
{
	setlocale(LC_CTYPE, "rus");
	srand(time(0));

	Graph graph(8);
	initializeGraph(graph);

	graph.printAdjMatrix();

	char choice;
	bool editing = true;

	while (editing)
	{
		std::cout << "\n1. Добавить город\n";
		std::cout << "2. Удалить город\n";
		std::cout << "3. Показать матрицу смежности\n";
		std::cout << "4. Выполнить генетический алгоритм\n";
		std::cout << "Выберите действие: ";
		std::cin >> choice;
		std::cin.clear();
		std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');

		switch (choice)
		{
		case('1'):
		{
			graph.addCity();
			graph.printAdjMatrix();
			break;
		}
		case('2'):
		{
			int city;
			std::ostringstream oss;

			oss << "Введите номер города для удаления (1 - " << graph.getVerticesCount() << "): ";
			//std::cout << oss.str();
			//std::cin >> city;
			ValidatedIntInput(oss.str(), city, "Номер города должен быть натуральным числом!");
			if (graph.getVerticesCount() > 2)
			{
				graph.deleteCity(city);
				graph.printAdjMatrix();
			}
			else
			{
				std::cout << "Нельзя удалить город! Должно остаться минимум 2\n";
			}
			break;
		}
		case('3'):
		{
			graph.printAdjMatrix();
			break;
		}
		case('4'):
		{
			editing = false;
			break;
		}
		default:
		{
			std::cout << "Неверный выбор\n";/*
			std::cin.clear();
			std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');*/
		}
		}
	}

	if (!graph.isCompleteGraph())
	{
		std::ostringstream oss;
		oss << "\nГраф не полный, возможны ошибки\nПродолжить?\n1.Да\n2.Нет\nВыбор: ";
		//std::cout << oss.str();
		int _choice;
		//std::cin >> _choice;
		ValidatedIntInput(oss.str(), _choice, "Выбор должен быть 1 или 2");
		if (_choice != 1)
		{
			return 0;
		}
	}

	int populationSize, mutationRate, crossoversPerGeneration, generations;

	std::ostringstream oss;
	oss << "\n\nРазмер начальной популяции (>= 2): ";
	ValidatedIntInput(oss.str(), populationSize, "Начальная популяция должна быть натуральным число >= 2", 2);

	oss.str("");
	oss.clear();
	oss << "Вероятност мутации (0 - 100)%: ";
	ValidatedIntInput(oss.str(), mutationRate, "Вероятность мутации должна быть натуральным число в диапазоне 0 - 100", 0, 100);

	oss.str("");
	oss.clear();
	oss << "Количество скрещиваний за поколение: ";
	ValidatedIntInput(oss.str(), crossoversPerGeneration, "Количество скрещиваний должно быть натуральным числом >= 1", 1);

	oss.str("");
	oss.clear();
	oss << "Количество поколений: ";
	ValidatedIntInput(oss.str(), generations, "Количество поколений должно быть натуральным числом >= 1", 1);

	std::vector<Individ> population;
	const std::vector<std::vector<int>>& adjMatrix = graph.getAdjMatrix();
	int citiesCount = graph.getVerticesCount();

	std::cout << "Генерация начальной популяции\n";
	while (population.size() < populationSize)
	{
		Individ individual(citiesCount);
		individual.generateRandomRoute(adjMatrix);

		if (std::find(population.begin(), population.end(), individual) == population.end())
		{
			population.push_back(individual);
			std::cout << "Добавлена особь с длиной маршрута: " << individual.routeSum << '\n';
		}
	}

	sortPopulation(population);
	std::cout << "Начальная отсортировання популяция\n";
	for (size_t i = 0; i < population.size(); i++)
	{
		std::cout << i + 1 << ". Длина: " << population[i].routeSum << " | Маршрут: ";
		for (int city : population[i].route)
		{
			std::cout << city << ' ';
		}
		std::cout << '\n';
	}

	for (int gen = 1; gen <= generations; gen++)
	{
		std::cout << "\nПоколение " << gen << '\n';

		for (int cross = 0; cross < crossoversPerGeneration; cross++)
		{
			int parent1Idx = rand() % populationSize;
			int parent2Idx;
			do
			{
				parent2Idx = rand() % populationSize;
			} while (parent1Idx == parent2Idx);

			Individ child1(citiesCount);
			Individ child2(citiesCount);

			child1.orderedCrossover(population[parent1Idx], population[parent2Idx], mutationRate, adjMatrix);
			child2.orderedCrossover(population[parent2Idx], population[parent1Idx], mutationRate, adjMatrix);

			if (child1.routeSum < INT_MAX && std::find(population.begin(), population.end(), child1) == population.end())
			{
				population.push_back(child1);
			}
			if (child2.routeSum < INT_MAX && std::find(population.begin(), population.end(), child1) == population.end())
			{
				population.push_back(child2);
			}
		}

		sortPopulation(population);

		while (population.size() > populationSize)
		{
			population.pop_back();
		}

		std::cout << "Лучшая длина маршрута: " << population[0].routeSum << '\n';
		std::cout << "Средняя длина: ";
		double avg = 0;
		for (const auto& ind : population)
		{
			avg += ind.routeSum;
		}
		avg /= population.size();
		std::cout << avg << '\n';

		if (gen % 5 == 0 || gen == generations)
		{
			std::cout << "Лучший маршрут поколения " << gen << ": ";
			for (int city : population[0].route)
			{
				std::cout << city << ' ';
			}
			std::cout << '\n';
		}
	}

	std::cout << "\n\n\nНаименьшая длина маршрута: " << population[0].routeSum << '\n';
	std::cout << "Оптимальный результат: ";
	for (int city : population[0].route)
	{
		std::cout << city << ' ';
	}

	std::cout << population[0].route[0] << '\n';

	std::cout << "Найденные маршруты:\n";
	for (int i = 0; i < population.size(); i++)
	{
		std::cout << i + 1 << ". Длина: " << population[i].routeSum << " | Маршрут: ";
		for (int city : population[0].route)
		{
			std::cout << city << ' ';
		}
		std::cout << population[i].route[0] << '\n';
	}

	return 0;
}