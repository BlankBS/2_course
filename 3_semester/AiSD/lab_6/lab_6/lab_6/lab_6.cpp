#include <iostream>
#include <string>
#include <map>
#include <list>
#include <vector>

struct Node
{
	int number;
	std::string symbol = "";
	Node* left, * right;
};

Node* HuffmanSolve(std::list<Node*> nodes)
{
	while (nodes.size() != 1)
	{
		nodes.sort([](Node* firstNode, Node* secondNode) -> bool
			{
				return firstNode->number < secondNode->number;
			});

		Node* left = nodes.front();
		nodes.pop_front();

		Node* right = nodes.front();
		nodes.pop_front();

		Node* parent = new Node;
		parent->left = left;
		parent->right = right;
		parent->symbol += left->symbol + right->symbol;
		parent->number = left->number + right->number;

		nodes.push_back(parent);
	}

	return nodes.front();
}

void BuildHuffmanTree(Node* root, std::vector<bool>& code, std::map<char, std::vector<bool>>& matchingTable)
{
	if (root->left != nullptr)
	{
		code.push_back(0);
		BuildHuffmanTree(root->left, code, matchingTable);
	}

	if (root->right != nullptr)
	{
		code.push_back(1);
		BuildHuffmanTree(root->right, code, matchingTable);
	}

	if (root->left == nullptr && root->right == nullptr)
	{
		matchingTable[root->symbol[0]] = code;
	}

	if (!code.empty())
	{
		code.pop_back();
	}
}

int main()
{
	setlocale(LC_CTYPE, "rus");

	std::map<char, int> counter;
	std::map<char, std::vector<bool>> matchingTable;
	std::list<Node*> nodes;
	std::vector<bool> code;

	std::cout << "Строка: ";
	std::string text;
	std::getline(std::cin, text);

	int size = text.size();
	for (int i = 0; i < size; i++)
	{
		counter[text[i]]++;
	}

	std::cout << "\nЧастота символов\n";
	for (auto it = counter.begin(); it != counter.end(); it++)
	{
		std::cout << it->first << " -> " << it->second << '\n';
		Node* node = new Node;
		node->symbol += it->first;
		node->number = it->second;
		node->left = nullptr;
		node->right = nullptr;
		nodes.push_back(node);
	}

	std::cout << '\n';

	Node* root = HuffmanSolve(nodes);
	BuildHuffmanTree(root, code, matchingTable);

	std::cout << "\nКоды Хаффмана\n";
	std::vector<bool>& temp = matchingTable.at(text[0]);
	int sizeTemp;
	
	for (const auto& it : matchingTable)
	{
		std::cout << it.first << " = ";
		sizeTemp = it.second.size();
		for (int j = 0; j < sizeTemp; j++)
		{
			std::cout << it.second[j];
		}
		std::cout << '\n';
	}

	std::cout << "\nЗакодированное сообщение\n";
	for (int i = 0; i < size; i++)
	{
		temp = matchingTable.at(text[i]);
		sizeTemp = temp.size();
		for (int j = 0; j < sizeTemp; j++)
		{
			std::cout << temp[j];
		}
	}

	return 0;
}