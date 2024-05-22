# GitHub Helper

GitHub Helper to program, który zawiera opis komend w Git. Program pozwala u¿ytkownikowi wpisaæ numer komendy, któr¹ chce zobaczyæ, a nastêpnie wyœwietla mu szczegó³owe informacje o danej komendzie.

## Jak to dzia³a

Program ³¹czy siê z baz¹ danych SQLite za pomoc¹ klasy `DatabaseConnection` i wykonuje zapytania SQL, aby pobraæ listê dostêpnych komend Git, ich parametrów i przyk³adów u¿ycia.

U¿ytkownik wprowadza numer komendy, któr¹ chce u¿yæ, a program wyœwietla szczegó³owe informacje o tej komendzie, takie jak jej sk³adnia, opis, dostêpne parametry i przyk³ady u¿ycia.

Jeœli u¿ytkownik wprowadzi niepoprawn¹ komendê, program wyœwietli komunikat o b³êdzie i poprosi o wprowadzenie poprawnej komendy.

## U¿ycie

1. Uruchom program.
2. Zobaczysz listê dostêpnych komend Git.
3. Wpisz numer komendy, aby wyœwietliæ jej szczegó³y.
4. Jeœli chcesz zakoñczyæ, wpisz s³owo 'koniec'.

## Architektura

Program korzysta z klasy `DatabaseConnection` do nawi¹zania po³¹czenia z baz¹ danych SQLite i wykonania zapytañ SQL. Wyniki zapytañ s¹ przechowywane w s³owniku i wykorzystywane do wyœwietlania dostêpnych komend Git oraz ich szczegó³ów.

## Wymagania

Program wymaga dostêpu do bazy danych SQLite z tabelami `KomendyGit`, `ParametryKomend` i `PrzykladyKomend`. Baza danych jest obs³ugiwana przez klasê `DatabaseConnection`, która otwiera po³¹czenie z baz¹ danych, wykonuje zapytania SQL i zwraca wyniki. Dodatkowo, program korzysta z pakietu NuGet SQLite w Visual Studio do obs³ugi bazy danych SQLite.
