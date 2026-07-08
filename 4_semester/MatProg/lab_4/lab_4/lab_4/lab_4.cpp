#include <iostream>
#include <string>
#include <algorithm>
#include <vector>

using namespace std;

// Рекурсивный LCS (как на стр. 9 файла)
int lcs_recursive(int lenx, const char x[], int leny, const char y[]) {
    if (lenx == 0 || leny == 0) return 0;
    if (x[lenx - 1] == y[leny - 1])
        return 1 + lcs_recursive(lenx - 1, x, leny - 1, y);
    else
        return max(lcs_recursive(lenx - 1, x, leny, y), lcs_recursive(lenx, x, leny - 1, y));
}

// ДП версия (логика со стр. 10-11)
int lcs_dp(string x, string y) {
    int m = x.length();
    int n = y.length();
    vector<vector<int>> L(m + 1, vector<int>(n + 1));

    for (int i = 0; i <= m; i++) {
        for (int j = 0; j <= n; j++) {
            if (i == 0 || j == 0)
                L[i][j] = 0;
            else if (x[i - 1] == y[j - 1])
                L[i][j] = L[i - 1][j - 1] + 1;
            else
                L[i][j] = max(L[i - 1][j], L[i][j - 1]);
        }
    }
    return L[m][n];
}

int main() {
    setlocale(LC_ALL, "rus");

    const char* x = "ANPAFRE";
    const char* y = "ICBUFR";

    cout << "Последовательность X: " << x << endl;
    cout << "Последовательность Y: " << y << endl;

    int res_r = lcs_recursive(7, x, 6, y);
    int res_dp = lcs_dp(x, y);

    cout << "Длина LCS (рекурсия): " << res_r << endl;
    cout << "Длина LCS (дин. прогр.): " << res_dp << endl;

    return 0;
}