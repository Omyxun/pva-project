# Návrh

## Seznam tříd

| Třída | Co uchovává | Co dělá |
| --- | --- | --- |
| `Hrac` | Jméno a přezdívku hráče. | Představuje jednoho hráče. |
| `SpravaHracu` | Slovník hráčů podle přezdívky. | Přidává, odebírá a vyhledává hráče. Hlídá jedinečnost přezdívek. |
| `Klub` | Evidenci hráčů a aktuální turnaj. | Zakládá a ruší turnaj, přihlašuje hráče a kontroluje jejich odebrání. |
| `Turnaj` | Název, disciplínu, počet kol, přihlášené hráče a kola. | Vytváří kola, kontroluje dvojice a určuje hráče s volným losem. Předává výsledky aktuálnímu kolu. |
| `Kolo` | Číslo, stav, zápasy a případného hráče s volným losem. | Hlídá, aby byl hráč v kole nejvýš jednou. Umožňuje zadat výsledky a uzavřít kolo. |
| `Zapas` | Číslo zápasu, oba hráče, výsledek a odkaz na kolo. | Nastavuje výsledek a kontroluje, zda ho stav kola dovoluje změnit. |
| `Bodovani` | Konstanty pro výhru, remízu, prohru a volný los. | Drží pravidla bodování na jednom místě. |
| `RadekTabulky` | Pořadí, hráče, body a počet odehraných zápasů. | Představuje jeden řádek výsledné tabulky. |
| `Tabulka` | Vlastní data neuchovává. | Z výsledků vypočítá body a počty zápasů. Řádky seřadí podle bodů a potom podle přezdívky. |
| `Uloziste` | Cestu k souboru a hlavičku formátu. | Ukládá a načítá celý stav klubu. Kontroluje soubor a při chybě načítání uvede číslo řádku. |
| `Menu` | Klub a úložiště, se kterými program pracuje. | Zobrazuje nabídku, volá metody logiky a vypisuje výsledky i chyby. |
| `Vstup` | Vlastní data neuchovává. | Načítá text, čísla a odpovědi ano/ne. Při chybném vstupu se zeptá znovu. |
| `Validace` | Vlastní data neuchovává. | Kontroluje, že text není prázdný a neobsahuje středník. |
| `TurnajException` | Zprávu o chybě. | Používá se pro chyby, které má program oznámit uživateli. |

Klub má jednu evidenci hráčů a nejvýš jeden aktuální turnaj. Turnaj obsahuje kola a každé kolo obsahuje zápasy. Zápasy odkazují na hráče z evidence.

Turnajová logika pracuje s daty a při chybě vyhodí `TurnajException`. Vypisování a ovládání programu řeší `Menu` a `Vstup`. Soubor `Program.cs` program spouští a zachytává konec vstupu.

## Formát ukládání

Program ukládá stav do textového souboru `turnaj.txt` v UTF-8. První řádek je vždy `TURNAJOVY_SYSTEM;1`. Podle hlavičky se pozná, že soubor patří turnajovému systému, a číslo `1` označuje verzi formátu.

Každý další řádek obsahuje jeden záznam. První položka určuje jeho typ a středník odděluje jednotlivá pole. Středník je proto v textových vstupech zakázaný.

| Typ | Pole za typem | Význam |
| --- | --- | --- |
| `HRAC` | přezdívka, jméno | Hráč evidovaný v klubu. Přezdívka je jedinečný identifikátor. |
| `TURNAJ` | název, disciplína, plánovaný počet kol | Aktuální turnaj. Pokud žádný není založený, za hráči už další záznamy nejsou. |
| `PRIHLASEN` | přezdívka hráče | Odkaz na hráče z evidence, který je přihlášený do turnaje. |
| `KOLO` | číslo kola, stav | Stav má hodnotu `0` při losování, `1` při hraní a `2` po uzavření. |
| `ZAPAS` | číslo zápasu, přezdívka prvního hráče, přezdívka druhého hráče, výsledek | Zápas patří k poslednímu uvedenému kolu. Výsledek `0` znamená nezadaný výsledek, `1` výhru prvního hráče, `2` výhru druhého a `3` remízu. |

### Ukázka souboru

~~~text
TURNAJOVY_SYSTEM;1
HRAC;anna;Anna Nováková
HRAC;boris;Boris Malý
HRAC;cilka;Cecílie Veselá
TURNAJ;Školní turnaj;Šachy;3
PRIHLASEN;anna
PRIHLASEN;boris
PRIHLASEN;cilka
KOLO;1;2
ZAPAS;1;anna;boris;3
KOLO;2;1
ZAPAS;1;boris;cilka;0
~~~

V prvním kole Anna s Borisem remizovali a Cecílie měla volný los. Ve druhém kole má volný los Anna a zápas Borise s Cecílií čeká na výsledek. Volný los se do souboru nezapisuje. Po dokončení losování se dá určit podle toho, který přihlášený hráč v kole nemá zápas. Při stavu `0` mohou dvojice ještě přibývat.

### Načtení a kontrola souboru

Záznamy jsou seřazené tak, aby byly údaje o hráčích k dispozici dřív než odkazy na ně. Nejdřív se čtou hráči, potom turnaj a přihlášky a nakonec kola se zápasy. Evidence hráčů používá `Dictionary<string, Hrac>` s přezdívkou jako klíčem. Podle přezdívky lze přímo najít hráče z přihlášky nebo zápasu bez procházení celé evidence.

Načítání má podle typu kontrolovat počet polí a platnost jejich hodnot. Za poškozený se považuje například soubor s neplatnou hlavičkou, neznámým typem záznamu, odkazem na neexistujícího hráče nebo uzavřeným kolem bez všech výsledků. Mezi chyby patří také opakovaná přezdívka v evidenci, dvojí přihlášení stejného hráče, duplicitní číslo kola či zápasu v rámci stejného kola nebo tentýž hráč ve dvou zápasech jednoho kola. Poškozený soubor se odmítne a uživatel dostane chybovou zprávu. Chybějící soubor lze poznat samostatně kontrolou jeho existence.

### Proč tento formát

Soubor jde otevřít v běžném textovém editoru a přečíst po řádcích bez další knihovny. Typ na začátku řádku říká, jaká pole má program čekat. Přezdívky v přihláškách a zápasech odkazují na jediný záznam hráče, takže se jména neopakují. Hlavička s verzí umožní při budoucí změně formátu rozeznat starší uložené soubory.
