# Documentation

User stories
- As a < player >, I want < have a overview of the board > so that < I can keep track of the game >.
- As a < player >, I want < a clear descripted layout of the game > so that < I can navigate in the game >.
- As a < player >, I want < to have access to the game rules > so that < I can play the game correctly >.
- As a < player >, I want < to have the options displayed > so that < I can organize my game >.
- As a < player >, I want < keep track of the others player movement > so that < I can beat them in the game >.
- As a < player >, I want < to have the results displayed and stored > so that < I can keep play if the game gets interrupted>.


- Vad behöver vi?
- - En tärning.
- - 2-4 spelare.
- - En spelplan.
- - En spelmotor.
- - Resultattavla.
- - Spelregler.
- - Spelpjäser (4 per spelare).
- - Spar/laddfunktion.
- - Pjäsbo.
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

2020-03-30 Introduktion och planering till Fia-spelet

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

2020-03-31 Introduktion och planering till Fia-spelet

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
