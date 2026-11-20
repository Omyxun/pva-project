# Návrh

## Seznam tříd

doplní Ondra

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
