
# GitHub Helper

## Przegląd

GitHub Helper to aplikacja napisana w .NET, zaprojektowana w celu pomagania użytkownikom w korzystaniu z komend i parametrów GitHub. Aplikacja korzysta z bazy danych SQLite do przechowywania i pobierania informacji o komendach, parametrach i przykładach.

## Spis treści

- [Przegląd](#przegląd)
- [Funkcje](#funkcje)
- [Struktura bazy danych](#struktura-bazy-danych)
- [Instalacja](#instalacja)
- [Użycie](#użycie)
- [Licencja](#licencja)

## Funkcje

- Udostępnia listę komend Git.
- Wyświetla parametry związane z każdą komendą.
- Pokazuje przykłady użycia każdej komendy.

## Struktura bazy danych

Aplikacja korzysta z bazy danych SQLite (`GitHub_Helper.db`) z następującymi tabelami:

1. **KomendyGit**: Przechowuje komendy Git.
2. **ParametryKomend**: Przechowuje parametry dla każdej komendy.
3. **PrzykladyKomend**: Przechowuje przykłady dla każdej komendy.

![Struktura bazy danych](./path_to_image/image.png)

## Instalacja

Wykonaj poniższe kroki, aby skonfigurować i uruchomić aplikację:

1. **Sklonuj repozytorium**
   ```bash
   git clone https://github.com/JaegerMajster/56205_51417_55245.git
   ```

2. **Przejdź do katalogu projektu**
   ```bash
   cd github-helper
   ```

3. **Otwórz projekt w Visual Studio**
   Otwórz `GitHub Helper.csproj` w Visual Studio.

4. **Przywróć pakiety NuGet**
   Visual Studio powinno automatycznie przywrócić niezbędne pakiety NuGet. Jeśli nie, przywróć je ręcznie, używając:
   ```bash
   dotnet restore
   ```

5. **Zbuduj projekt**
   Zbuduj projekt, wybierając `Build` > `Build Solution` w Visual Studio lub używając polecenia:
   ```bash
   dotnet build
   ```

## Użycie

Aby uruchomić aplikację, możesz pobrać najnowszą wersję z [Releases page](https://github.com/JaegerMajster/56205_51417_55245/releases/tag/v0.98) lub zbudować i uruchomić ją z kodu źródłowego.

### Uruchamianie z kodu źródłowego

Aby uruchomić aplikację z kodu źródłowego, użyj funkcji debugowania Visual Studio lub wykonaj następujące polecenie w terminalu:
```bash
dotnet run --project GitHub Helper.csproj
```

Upewnij się, że plik bazy danych SQLite (`GitHub_Helper.db`) znajduje się w tym samym katalogu co plik wykonywalny lub podaj właściwą ścieżkę w stringu połączenia w `DatabaseConnection.cs`.

### Uruchamianie z wersji

1. **Pobierz najnowszą wersję**
   Pobierz najnowszą wersję z [Releases page](https://github.com/JaegerMajster/56205_51417_55245/releases/tag/v0.98).

2. **Wypakuj pobrane archiwum**
   Wypakuj zawartość pobranego archiwum.

3. **Uruchom plik wykonywalny**
   Przejdź do wypakowanego folderu i uruchom plik `GitHub Helper.exe`.

## Licencja

Ten projekt jest licencjonowany na podstawie licencji MIT - zobacz plik [LICENSE](LICENSE) po więcej szczegółów.
