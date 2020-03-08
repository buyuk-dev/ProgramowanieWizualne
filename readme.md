Projekt zaliczeniowy - Programowanie Wizualne
---------------------------------------------
Michał Michalski 109679


## Opis składowych aplikacji:

1. ProgWizApp:
Projekt zawierający aplikację.

2. DAOSql:
Biblioteka zawierająca klasy ładujące dane z bazy SQLite.

3. DAOMock:
Biblioteka zawierająca klasy łądujące przykładowe dane zapisane w kodzie.

4. CORE:
Biblioteka zawierająca typ wyliczeniowy ViolinState oraz rozszerzoną implementację klasy BindingList która przechwytuje zdarzenie usuwania elementu przed jego faktycznym usunięciem co pozwala wykonać na nim dodatkowe operacje.

5. INTERFACES:
Biblioteka zawierająca wszystkie interfejsy aplikacji:

  - IObjectStorage: interfejs dostępu do danych z bazy, implementowany przez klasy Storage w bibliotekach DAO.
  - IViolinModel, IMakerModel: interfejsy reprezentujące obiekty producenta i produktu w aplikacji.

6. BLC:
Z powodu ograniczonej funkcjonalności aplikacji warstwa loigiki biznesowej pozostała pusta. Cała logika sprowadza się do podłączenia danych do
tabeli, co uznałem za zadanie bardziej pasujące do zadań ViewModel niż BLC.

7. Settings:
Ustawienia aplikacji są przechowywane w pliku konfiguracyjnym ProgWizApp.exe.config, który można edytować bez rekompilacji projektu.
  
  - SqliteUri: ścieżka bezwzględna do pliku bazy danych SQLite (przykładową bazę można utworzyć skryptem create_tables.sql)
  - DaoUri: ścieżka bezwzględna do pliku dll wybranej biblioteki DAO (DAOMock.dll lub DAOSql.dll)
  - AppTitle: nazwa aplikacji wyświetlana na pasku tytułowym


## Opis interfeju użytkownika:

Okno aplikacji podzielone zostało na dwie zakładki: Violins / Makers.
W każdej z zakładek znajduje się tabela wyświetlająca rekordy zapisane w bazie danych, oraz pole tekstowe służące jako prosty filtr do tabeli.
Wprowadzenie tekstu w polu filtra powoduje filtrację rekordów po nazwie producenta.
Do tabeli dodawać można nowe rekordy poprzez pusty wiersz na dole tabeli.
Klawiszem DELETE można usuwać wybrany aktualnie rekord.
Wszystkie rekordy wyświetlane tabeli można bezpośrednio edytować wpisując lub wybierając nową wartość w odpowiednim polu wybranego rekordu.
Wszystkie zmiany zostają natychmiast zapisane w odpowiedniej bazie danych.