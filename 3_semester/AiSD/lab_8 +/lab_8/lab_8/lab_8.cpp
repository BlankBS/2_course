#include <iostream>
#include <vector>
#include <string>
#include <algorithm>
#include <iomanip>
#include <limits>

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

void ValidatedIntInput(std::string str1, int& EnterVariable, std::string str2)
{
	do
	{
		isMistake = false;
		std::cout << str1;
		std::cin >> EnterVariable;
		if (EnterVariable <= 0)
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
		ValidatedIntInput(
			"Введите количество предметов: ",
			count,
			"Количество предметов должно быть натуральным числом!"
		);

		int cost, weight;
		std::string name;
		for (int i = 0; i < count; ++i) {

			std::cout << "\nПредмет " << i + 1 << ":\n";
			std::cout << "Название: ";
			std::cin >> name;
			ValidatedIntInput(
				"Стоимость: ",
				cost,
				"Цена должна быть положительным числом!"
			);
			ValidatedIntInput(
				"Вес: ",
				weight,
				"Вес должен быть положительным числом!"
			);

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
		std::cout << "\n\n\n";
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
		std::cout << "1. Ввести данные и решить задачу\n";
		std::cout << "2. Выход\n";
		std::cout << "Выберите действие: ";
		std::cin >> choice;

		if (checkOnLetter())
		{
			system("cls");
			std::cout << "Неверный выбор!\n";
			continue;
		}
		isMistake = false;

		switch (choice) {
		case 1: {
			if (isMistake)
			{
				std::cout << "\n\nОшибка!\n\n";
				system("cls");
				break;
			}
			int capacity;
			ValidatedIntInput(
				"Введите вместимость рюкзака: ",
				capacity,
				"Ошибка. Вместимость должна быть натуральным числом!"
			);

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
		{
			system("cls");
			std::cout << "Неверный выбор!\n";
		}
		}
	} while (choice != 2);

	return 0;
}