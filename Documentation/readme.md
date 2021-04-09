# Documentation

User stories
- As a < player >, I want < have a overview of the board > so that < I can keep track of the game >.
- As a < player >, I want < a clear descripted layout of the game > so that < I can navigate in the game >.
- As a < player >, I want < to have access to the game rules > so that < I can play the game correctly >.
- As a < player >, I want < to have the options displayed > so that < I can organize my game >.
- As a < player >, I want < keep track of the others player movement > so that < I can beat them in the game >.
- As a < player >, I want < to have the results displayed and stored > so that < I can keep play if the game gets interrupted>.


- Vad behöver vi?
- - En tärning. - DONE!
- - 2-4 spelare. - FÖRBEREDD
- - En spelplan. - DONE!
- - En spelmotor. - DONE!
- - Resultattavla. - FÖRBEREDD
- - Spelregler. - FÖRBEREDD
- - Spelpjäser (4 per spelare). - DONE!
- - Spar/laddfunktion.
- - Pjäsbo. - DONE!
- - Start/slut.


- Kravlista
- - Konsolgränssnitt
- - Klassbibliotek (spelmotor). All logik som rör spelet ska placeras här.
- - Enhetstester.
- - Föra dagbok.
- - Videopresentation.
- - Entity Framework, code first.
- - Kunna köra lokalt, inga externa databaser.
- - Flera spel ska kunna vara igång. 
- - Dokumentation i form av User Stories, CRC cards, Use Case diagram.


- Elementlista
- - Meny class(skapa nytt spel/välj spelare/ladda/)
- - Gameboard class(positioner, start/slut, synkad med drawfunktionen)
- -- Antal rutor, 
- - Rules class
- - Player class(placera pjäser?)
- - - GamePieces, Color, Amount, Referenst till
- - Nest
- -- Player, GamePieces
- - GamePieces class
- - - Amount, Color, Player, 
- - Dice class(Vilken klass löser vem turs? )
- - - Randomfunktion, Startkast
- - GameLogic(movement, sonderar planen, kontrollfunktion, validering)
- - Game class(information kring pågående spel, navigering, antal spelare, poäng, loggar information till DB)
- - Draw element(Skriver ut varje rad till brädet)
- - Databas(load/save funktion)
- - Enhetstest

2021-03-30 Introduktion och planering till Fia-spelet

---

- Gjort i dag
- - Skrivit 6 st userstories.
- - Vad vi behöver för delar i programmet lista
- - Kortfattad kravlista med saker att tänka på under arbetets gång.
- - Planering för datastrukturen såsom klasser med egenskaper och funktioner
- -  Arbetsupplägg med metoder

- Planering till morgondagen
- - Implementera en programstruktur med klass modeller
- - Få in enhetstester
- - Installera externa moduler såsom Entity Framework
- - Databasschemat

2021-03-31 Introduktion och planering till Fia-spelet

---

- Gjort i dag
- - Mattias anmälde sig till och göra en scrollbarmeny
- - UseCase gjorda
- - CRC cards påbörjade
- - Implementerat klasser och modeller
- - Krattat väg för databas
- - Implementerat ett enhetstest till projektet

- Planering till morgondagen
- - Påminna Mattias om att han anmält sig till att göra en scrollbarmeny
- - Lära oss mer om tester 
- - Forsätta koda
- - Hitta en bra grund till drawing
- - Lära oss om multipla char arrayer av mån av tid

2021-04-01 Introduktion och planering till Fia-spelet

--- 

- Gjort idag
- - Jens har påminnt Mattias om Meny
- - Skapat Test som testar Dice!!!
- - Implementerat Interface till Game
- - Skapat tvådimensionell Gameboard

- Planering till 5/4
- - Interface för object på vårt gameboard. Byta ut object till interfacet.

2021-04-06

---

- Gjort idag
- - Mattias påminde sig själv om Meny
- - Skapat och implementerat ett interface för BoardObject
- - Strukturerat upp färgerna på vårt GameBoard
- - Påbörjat en GameLoop där vi kan RollDice, välja vilken GamePiece som ska flyttas samt rotera bland spelarna
- - Jens har lagt in lite fler enhetstester

- Planering till 7/4 (Belindas födelsedag!!)
- - Skriva fler enhetstester.
- - Lösa så att våra GamePieces faktiskt kan flytta på sig.
- - Jens vill pilla med sina färger. 
- - Strukturera upp databasen?
- - Resultatbräde

2021-04-07

---

- Gjort idag
- - Belinda har fått tårta.
- - Mattias skapar en fantastiskt användarvänlig meny.
- - Skapat RoadMap inför navigering av GamePieces.
- - Vi har samlat in alla Paths på vårt GameBoard i en lista som våra GamePieces ska kunna navigera på.
- - Försöker ordna upp våra rader och kolumner i Position, skriver om listan?
- - ListCeption är påbörjat!

- Planering till 8/4
- - Finslipa våra metoder som får GamePieces att flytta på sig.
- - Enhetstester!!
- - Databas om tid finns
- - Resultatbräde om tid finns

2021-04-08

---

- Gjort idag
- - Movement på plats
- - Städat
- - Meny på plats (Jens tår i ögat)
- - 

- Planering till 9/4
- - Fixa validering till movement
- - Result/tavla

2021-04-09

---

- Gjort idag
- - Movement logik.
- - Movement logik..
- - Movement logik...
- - Validering till movement
- - Snart kan våra gamepieces vandra in mot mål!

- Planering till 11/4
- - Skriva kommentarer och summaries
- - Få klart programmet
