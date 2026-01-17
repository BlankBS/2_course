#include <iostream>
#include "ValidationLibrary.h"
#include <vector>	
#include <cmath>
#include <algorithm>
#include <limits>
#include <iomanip>
#include <random>
#include <ctime>

class AntColony
{
private:
	int numCities;
	std::vector<std::vector<double>> distanceMatrix;
	std::vector<std::vector<double>> pheromoneMatrix;
	std::vector<std::vector<double>> visibilityMatrix;

	double initialPheromone;
	double alpha;
	double beta;
	double evaporationRate;
	int numAnts;
	int numIterations;

	std::mt19937 gen;
	std::uniform_real_distribution<> dis;

public:
	AntColony(int _numCities, double _initialPheromone, double _alpha, double _beta, double _numIterations) :
		numCities(_numCities), initialPheromone(_initialPheromone), alpha(_alpha), beta(_beta), numIterations(_numIterations)
	{
		std::random_device rd;
		gen = std::mt19937(rd());
		dis = std::uniform_real_distribution<>(0.0, 1.0);
		
		evaporationRate = 0.5;
		numAnts = numCities;

		distanceMatrix = std::vector<std::vector<double>>(numCities, std::vector<double>(numCities, 0.0));
		pheromoneMatrix = std::vector<std::vector<double>>(numCities, std::vector<double>(numCities, initialPheromone));
		visibilityMatrix = std::vector<std::vector<double>>(numCities, std::vector<double>(numCities, 0.0));

		GenerateDistanceMatrix();
		InitializeVisibilityMatrix();
	}

	void GenerateDistanceMatrix()
	{
		std::cout << "\nГенерация матрицы расстояний между городами:\n";
		for (int i = 0; i < numCities; i++)
		{
			for (int j = i + 1; j < numCities; j++)
			{
				double dist = 1.0 + dis(gen) * 99.0;
				distanceMatrix[i][j] = dist;
				distanceMatrix[j][i] = dist;
			}
			distanceMatrix[i][i] = 0.0;
		}

		PrintDistanceMatrix();
	}

	void InitializeVisibilityMatrix()
	{
		for (int i = 0; i < numCities; i++)
		{
			for (int j = 0; j < numCities; j++)
			{
				if (i != j)
				{
					visibilityMatrix[i][j] = 1.0 / distanceMatrix[i][j];
				}
				else
				{
					visibilityMatrix[i][j] = 0.0;
				}
			}
		}
	}

	void PrintDistanceMatrix() {
		std::cout << "\nМатрица расстояний:\n";
		std::cout << "     ";
		for (int i = 0; i < numCities; i++) {
			std::cout << (i >= 9 ? std::setw(4) : std::setw(5)) << "Г" << i + 1;
		}
		std::cout << "\n";

		for (int i = 0; i < numCities; i++) {
			std::cout << "Г" << std::setw(2) << i + 1 << "  ";
			for (int j = 0; j < numCities; j++) {
				std::cout << std::setw(6) << std::fixed << std::setprecision(1) << distanceMatrix[i][j];
			}
			std::cout << "\n";
		}
		std::cout << "\n";
	}

	std::vector<int> ConstructAntTour(int startCity)
	{
		std::vector<int> tour;
		std::vector<bool> visited(numCities, false);

		int currentCity = startCity;
		tour.push_back(currentCity);
		visited[currentCity] = true;

		for (int step = 1; step < numCities; step++)
		{
			int nextCity = SelectNextCity(currentCity, visited);
			tour.push_back(nextCity);
			visited[nextCity] = true;
			currentCity = nextCity;
		}

		tour.push_back(tour[0]);

		return tour;
	}

	int SelectNextCity(int currentCity, const std::vector<bool>& visited)
	{
		std::vector<double> probabilities(numCities, 0.0);
		double sum = 0.0;

		for (int i = 0; i < numCities; i++)
		{
			if (!visited[i])
			{
				double pheromone = std::pow(pheromoneMatrix[currentCity][i], alpha);
				double visibility = std::pow(visibilityMatrix[currentCity][i], beta);
				probabilities[i] = pheromone * visibility;
				sum += probabilities[i];
			}
		}

		for (int i = 0; i < numCities; i++)
		{
			if (!visited[i] && sum > 0)
			{
				probabilities[i] /= sum;
			}
		}

		double r = dis(gen);
		double cumulative = 0.0;


		for (int i = 0; i < numCities; i++)
		{
			if (!visited[i])
			{
				cumulative += probabilities[i];
				if (r <= cumulative || abs(cumulative - 1.0) < 1e-10)
				{
					return i;
				}
			}
		}

		for (int i = 0; i < numCities; i++)
		{
			if (!visited[i])
			{
				return i;
			}
		}

		return -1;
	}

	double CalculateTourLength(const std::vector<int>& tour)
	{
		double length = 0.0;
		for (size_t i = 0; i < tour.size() - 1; i++)
		{
			length += distanceMatrix[tour[i]][tour[i + 1]];
		}
		return length;
	}

	void UpdatePheromones(const std::vector<std::vector<int>>& antTours, const std::vector<double>& tourLengths)
	{
		for (int i = 0; i < numCities; i++)
		{
			for (int j = 0; j < numCities; j++)
			{
				if (i != j)
				{
					pheromoneMatrix[i][j] *= (1.0 - evaporationRate);
				}
			}
		}

		for (int ant = 0; ant < numAnts; ant++)
		{
			double pheromoneToAdd = 1.0 / tourLengths[ant];

			for (size_t i = 0; i < antTours[ant].size() - 1; i++)
			{
				int city1 = antTours[ant][i];
				int city2 = antTours[ant][i + 1];
				pheromoneMatrix[city1][city2] += pheromoneToAdd;
				pheromoneMatrix[city2][city1] += pheromoneToAdd;
			}
		}
	}

	void Run()
	{
		std::vector<int> bestTour;
		double bestTourLength = std::numeric_limits<double>::max();

		std::cout << "Параметры алгоритма:\n";
		std::cout << "- Количество городов: " << numCities << '\n';
		std::cout << "- Начальное значение феромонов: " << initialPheromone << '\n';
		std::cout << "- Альфа (влияние феромонов): " << alpha << '\n';
		std::cout << "- Бета (влияние видимости): " << beta << '\n';
		std::cout << "- Количество итераций: " << numIterations << '\n';
		std::cout << "- Количество муравьев: " << numAnts << '\n';
		std::cout << "- Скорость испарения: " << evaporationRate << '\n';

		for (int iteration = 0; iteration < numIterations; iteration++)
		{
			std::vector<std::vector<int>> antTours(numAnts);
			std::vector<double> tourLengths(numAnts);

			for (int ant = 0; ant < numAnts; ant++)
			{
				antTours[ant] = ConstructAntTour(ant % numCities);
				tourLengths[ant] = CalculateTourLength(antTours[ant]);

				if (tourLengths[ant] < bestTourLength)
				{
					bestTourLength = tourLengths[ant];
					bestTour = antTours[ant];
				}
			}

			UpdatePheromones(antTours, tourLengths);
			PrintIterationInfo(iteration + 1, bestTour, bestTourLength);
		}

		PrintFinalResult(bestTour, bestTourLength);
	}

	void PrintIterationInfo(int iteration, const std::vector<int>& bestTour, double bestLength)
	{
		std::cout << "\n--- Итерация " << iteration << " ---\n";
		std::cout << "Лучший маршрут: ";
		for (size_t i = 0; i < bestTour.size(); i++)
		{
			std::cout << 'Г' << bestTour[i] + 1;
			if (i < bestTour.size() - 1)
			{
				std::cout << " -> ";
			}
		}
		std::cout << " Длина маршрута: " << std::fixed << std::setprecision(2) << bestLength << '\n';
	}

	void PrintFinalResult(const std::vector<int>& bestTour, double bestLength)
	{
		std::cout << "\n\n\n\nОптимальный маршрут:\n";

		for (size_t i = 0; i < bestTour.size() - 1; i++)
		{
			std::cout << 'Г' << bestTour[i] + 1;
			if (i < bestTour.size() - 2)
			{
				std::cout << " -> ";
			}
		}
		std::cout << " -> Г" << bestTour[0] + 1;

		std::cout << "\n\nДетали маршрута:\n";
		double totalDistance = 0.0;
		for (size_t i = 0; i < bestTour.size() - 1; i++)
		{
			int city1 = bestTour[i];
			int city2 = bestTour[i + 1];
			double dist = distanceMatrix[city1][city2];
			std::cout << 'Г' << city1 + 1 << " -> Г" << city2 + 1 << ": " << std::fixed << std::setprecision(2) << dist << '\n';
			totalDistance += dist;
		}

		std::cout << "\nОбщая длина маршрута: " << std::fixed << std::setprecision(2) << bestLength << '\n';
	}
};

int main()
{
	setlocale(LC_CTYPE, "rus");

	int numCities;
	ValidatedIntInput("Количество городов (2 - 50): ", numCities, "Количество городов должны быть натуральным числом в диапазоне от 2 до 50!", 2, 50);
	double initialPheromone;
	ValidatedDoubleInput("Количество феромонов (0.1 - 10): ", initialPheromone, "Количество феромонов должно быть положительным числом в диапазоне от 0.1 до 10!", 0.1, 10);
	double alpha;
	ValidatedDoubleInput("Влияние феромонов (0.1 - 5): ", alpha, "Количество влияния феромонов должно быть положительным числом в диапазоне от 0.1 до 5!", 0.1, 5);
	double beta;
	ValidatedDoubleInput("Видимость феромонов (0.1 - 5): ", beta, "Количество влияния видимости должно быть положительным числом в диапазоне от 0.1 до 5!", 0.1, 5);
	int numIterations;
	ValidatedIntInput("Количество итераций (10 - 1000): ", numIterations, "Количество итераций должно быть натуральным числом в диапазоне от 10 до 1000!", 10, 1000);

	AntColony aco(numCities, initialPheromone, alpha, beta, numIterations);
	aco.Run();

	return 0;
}