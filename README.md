# GitHub Helper

GitHub Helper to program, kt�ry zawiera opis komend w Git. Program pozwala u�ytkownikowi wpisa� numer komendy, kt�r� chce zobaczy�, a nast�pnie wy�wietla mu szczeg�owe informacje o danej komendzie.

## Jak to dzia�a

Program ��czy si� z baz� danych SQLite za pomoc� klasy `DatabaseConnection` i wykonuje zapytania SQL, aby pobra� list� dost�pnych komend Git, ich parametr�w i przyk�ad�w u�ycia.

U�ytkownik wprowadza numer komendy, kt�r� chce u�y�, a program wy�wietla szczeg�owe informacje o tej komendzie, takie jak jej sk�adnia, opis, dost�pne parametry i przyk�ady u�ycia.

Je�li u�ytkownik wprowadzi niepoprawn� komend�, program wy�wietli komunikat o b��dzie i poprosi o wprowadzenie poprawnej komendy.

## Uruchomienie aplikacji

Aby uruchomi� aplikacj�, wykonaj nast�puj�ce kroki:

1. Sklonuj repozytorium lub pobierz pliki z repozytorium GitHub.
2. Przejd� do folderu `publish/win-x64`.
3. Uruchom plik `GitHub_Helper.exe`.
4. Zobaczysz list� dost�pnych komend Git.
5. Wpisz numer komendy, aby wy�wietli� jej szczeg�y.
6. Je�li chcesz zako�czy�, wpisz s�owo 'koniec'.

## Architektura

Program korzysta z klasy `DatabaseConnection` do nawi�zania po��czenia z baz� danych SQLite i wykonania zapyta� SQL. Wyniki zapyta� s� przechowywane w s�owniku i wykorzystywane do wy�wietlania dost�pnych komend Git oraz ich szczeg��w.

## Wymagania

Program wymaga dost�pu do bazy danych SQLite z tabelami `KomendyGit`, `ParametryKomend` i `PrzykladyKomend`. Baza danych jest obs�ugiwana przez klas� `DatabaseConnection`, kt�ra otwiera po��czenie z baz� danych, wykonuje zapytania SQL i zwraca wyniki.
