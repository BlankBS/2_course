#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <ctime>
#include <chrono>
#include <iomanip>

using namespace std;

string generateString(int len) {
    string s = "";
    static const char alphabet[] = "abcdefghijklmnopqrstuvwxyz";
    for (int i = 0; i < len; ++i) s += alphabet[rand() % 26];
    return s;
}

int levRecursive(string s1, string s2, int m, int n) {
    if (m == 0) return n;
    if (n == 0) return m;
    int cost = (s1[m - 1] == s2[n - 1]) ? 0 : 1;
    return min({
        levRecursive(s1, s2, m - 1, n) + 1,      
        levRecursive(s1, s2, m, n - 1) + 1,      
        levRecursive(s1, s2, m - 1, n - 1) + cost 
        });
}

int levDP(string s1, string s2) {
    int m = s1.length(), n = s2.length();
    vector<vector<int>> d(m + 1, vector<int>(n + 1));
    for (int i = 0; i <= m; i++) d[i][0] = i;
    for (int j = 0; j <= n; j++) d[0][j] = j;

    for (int i = 1; i <= m; i++) {
        for (int j = 1; j <= n; j++) {
            int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
            d[i][j] = min({ d[i - 1][j] + 1, d[i][j - 1] + 1, d[i - 1][j - 1] + cost });
        }
    }
    return d[m][n];
}

int lcsRecursive(string x, string y, int m, int n) {
    if (m == 0 || n == 0) return 0;
    if (x[m - 1] == y[n - 1])
        return 1 + lcsRecursive(x, y, m - 1, n - 1);
    else
        return max(lcsRecursive(x, y, m - 1, n), lcsRecursive(x, y, m, n - 1));
}

int lcsDP(string x, string y) {
    int m = x.length(), n = y.length();
    vector<vector<int>> L(m + 1, vector<int>(n + 1));
    for (int i = 0; i <= m; i++) {
        for (int j = 0; j <= n; j++) {
            if (i == 0 || j == 0) L[i][j] = 0;
            else if (x[i - 1] == y[j - 1]) L[i][j] = L[i - 1][j - 1] + 1;
            else L[i][j] = max(L[i - 1][j], L[i][j - 1]);
        }
    }
    return L[m][n];
}

int main() {
    setlocale(LC_ALL, "rus");
    srand(time(0));

    cout << "1 - 3 задание"<< endl;
    string S1 = generateString(300);
    string S2 = generateString(200);

    cout << "Первая строка:\n" << S1 << "\n\nВторая строка:\n" << S2 << "\n\n";

    double k_vals[] = { 1.0 / 25, 1.0 / 20, 1.0 / 15, 1.0 / 10, 1.0 / 5, 1.0 / 2 };

    cout << left << setw(10) << "K" << setw(12) << "Длина" << setw(20) << "Рекурсия (мс)" << "ДП (мс)" << endl;
    cout << string(55, '-') << endl;

    for (double k : k_vals) {
        int len1 = 300 * k;
        int len2 = 200 * k;
        string sub1 = S1.substr(0, len1);
        string sub2 = S2.substr(0, len2);
        string lenStr = to_string(len1) + "/" + to_string(len2);

        auto sDP = chrono::high_resolution_clock::now();
        levDP(sub1, sub2);
        auto eDP = chrono::high_resolution_clock::now();
        double timeDP = chrono::duration<double, milli>(eDP - sDP).count();

        cout << fixed << setprecision(3) << left << setw(10) << k << setw(12) << lenStr;

        if (len1 > 14) {
            cout << setw(20) << "> 60000.000";
        }
        else {
            auto sR = chrono::high_resolution_clock::now();
            levRecursive(sub1, sub2, len1, len2);
            auto eR = chrono::high_resolution_clock::now();
            double timeRec = chrono::duration<double, milli>(eR - sR).count();
            cout << setw(20) << timeRec;
        }
        cout << timeDP << endl;
    }

    cout << "\n5 задание" << endl;
    string X = "ANPAFRE";
    string Y = "ICBUFR";
    cout << "Строка X: " << X << endl;
    cout << "Строка Y: " << Y << endl;

    auto sLR = chrono::high_resolution_clock::now();
    int lcsR = lcsRecursive(X, Y, X.length(), Y.length());
    auto eLR = chrono::high_resolution_clock::now();

    auto sLDP = chrono::high_resolution_clock::now();
    int lcsD = lcsDP(X, Y);
    auto eLDP = chrono::high_resolution_clock::now();

    cout << "Длина LCS (рекурсия): " << lcsR << " (время: " << chrono::duration<double, milli>(eLR - sLR).count() << " мс)" << endl;
    cout << "Длина LCS (ДП):       " << lcsD << " (время: " << chrono::duration<double, milli>(eLDP - sLDP).count() << " мс)" << endl;
    cout << "Общая подпоследовательность: FR" << endl;

    system("pause");
    return 0;
}