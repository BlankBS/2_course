#pragma once 
#include <list> 
#include "Graph.h" 
namespace graph
{
	struct AList;
	struct AMatrix  // матрица смежности  
	{
		int  n_vertex;
		int* mr;
		AMatrix(int n);
		AMatrix(int n, int mr[]);
		AMatrix(const AMatrix& am);
		AMatrix(const AList& al);
		// количество вершин  
		// матрица   
		// создать нулевую матрицу n*n 
		// создать матрицу n*n и  
		// создать подобную матрицу 
		// создать матрицу из спискового  
		void set(int i, int j, int r);  // записать mr[i,j] = r 
		int  get(int i, int j)const;
		// элемент mr[i,j]  
	};
	struct AList  // списки смежности  
	{
		int  n_vertex;
		std::list<int>* mr;
		void create(int n);
		AList(int n);
		AList(int n, int mr[]);
		AList(const AMatrix& am);
		AList(const AList& al);
		void add(int i, int j);
		int  size(int i) const;
		// количество вершин  
		// массив списков 
		// создать массив пустых списков 
		// создать массив пустых списков  
		// создать списковое представление   
		// создать списковое представление  
		// создать подобную  структуру  
		// добавить в i-ый список 
		// размер i-го списка   
		int  get(int i, int j)const;   // j-ый  элемент i-го списка  
	};
};