# GitHub Helper

GitHub Helper to program, kt�ry zawiera opis komend w Git. Program pozwala u�ytkownikowi wpisa� numer komendy, kt�r� chce zobaczy�, a nast�pnie wy�wietla mu szczeg�owe informacje o danej komendzie.

## Jak to dzia�a

Program ��czy si� z baz� danych SQLite za pomoc� klasy `DatabaseConnection` i wykonuje zapytania SQL, aby pobra� list� dost�pnych komend Git, ich parametr�w i przyk�ad�w u�ycia.

U�ytkownik wprowadza numer komendy, kt�r� chce u�y�, a program wy�wietla szczeg�owe informacje o tej komendzie, takie jak jej sk�adnia, opis, dost�pne parametry i przyk�ady u�ycia.

Je�li u�ytkownik wprowadzi niepoprawn� komend�, program wy�wietli komunikat o b��dzie i poprosi o wprowadzenie poprawnej komendy.

## U�ycie

1. Uruchom program.
2. Zobaczysz list� dost�pnych komend Git.
3. Wpisz numer komendy, aby wy�wietli� jej szczeg�y.
4. Je�li chcesz zako�czy�, wpisz s�owo 'koniec'.

## Architektura

Program korzysta z klasy `DatabaseConnection` do nawi�zania po��czenia z baz� danych SQLite i wykonania zapyta� SQL. Wyniki zapyta� s� przechowywane w s�owniku i wykorzystywane do wy�wietlania dost�pnych komend Git oraz ich szczeg��w.

## Wymagania

Program wymaga dost�pu do bazy danych SQLite z tabelami `KomendyGit`, `ParametryKomend` i `PrzykladyKomend`. Baza danych jest obs�ugiwana przez klas� `DatabaseConnection`, kt�ra otwiera po��czenie z baz� danych, wykonuje zapytania SQL i zwraca wyniki. Dodatkowo, program korzysta z pakietu NuGet SQLite w Visual Studio do obs�ugi bazy danych SQLite.
