# User stories

## Kdo program používá

Program používá jen pořadatel turnaje (člen klubu, který turnaj vede). Hráči s programem nepracují, pořadateli jen hlásí výsledky. Proto jsou všechny stories z pohledu pořadatele.

## Stories

**US1** Jako pořadatel chci přidat hráče se jménem a přezdívkou, abych je mohl přihlašovat do turnajů.
- tasky: Přidání a výpis hráčů, Menu hráčů a turnaje

**US2** Jako pořadatel chci odebrat hráče, abych v seznamu neměl lidi, kteří už do klubu nechodí.
- tasky: Odebrání hráče, Menu hráčů a turnaje

**US3** Jako pořadatel chci vypsat všechny hráče, abych věděl, kdo je v klubu.
- tasky: Přidání a výpis hráčů, Menu hráčů a turnaje

**US4** Jako pořadatel chci založit turnaj s názvem, disciplínou a počtem kol, abych mohl začít soutěž.
- tasky: Založení turnaje a přihlašování přes klub, Menu hráčů a turnaje

**US5** Jako pořadatel chci přihlašovat a odhlašovat hráče do začátku prvního kola, abych měl správný seznam účastníků.
- tasky: Turnaj: přihlašování a odhlašování, Založení turnaje a přihlašování přes klub, Menu hráčů a turnaje

**US6** Jako pořadatel chci vytvořit kolo a sám určit dvojice, abych rozhodl, kdo s kým hraje.
- tasky: Kolo: dvojice a volný los, Turnaj: nové kolo a losování, Menu kol a výsledků

**US7** Jako pořadatel chci, aby hráč bez soupeře dostal volný los, abych nemusel lichý počet hráčů řešit ručně.
- tasky: Kolo: dvojice a volný los, Turnaj: nové kolo a losování

**US8** Jako pořadatel chci zadat a opravit výsledek zápasu, abych mohl opravit překlep.
- tasky: Zápas a jeho výsledek, Kolo: výsledky a uzavření, Menu kol a výsledků

**US9** Jako pořadatel chci uzavřít kolo, až mají všechny zápasy výsledek, aby se výsledky už neměnily.
- tasky: Kolo: výsledky a uzavření, Menu kol a výsledků

**US10** Jako pořadatel chci vidět tabulku s pořadím, body a počtem zápasů, abych věděl, jak turnaj stojí.
- tasky: Výpočet tabulky, Výpis tabulky, uložení a konec, Ověření tabulky na připraveném turnaji

**US11** Jako pořadatel chci uložit rozehraný turnaj a příště pokračovat, abych ho nemusel odehrát najednou.
- tasky: Uložení stavu do souboru, Načtení ze souboru a poškozený soubor, Výpis tabulky, uložení a konec

**US12** Jako pořadatel chci, aby program nespadl, když zadám nesmysl, abych nepřišel o rozehraný turnaj.
- tasky: Ošetřené vstupy z klávesnice, Načtení ze souboru a poškozený soubor

## Otevřené otázky na zákazníka

- Co se stane, když hráč během turnaje odstoupí? (zatím neřešíme, hráč musí turnaj dohrát)
- Jak rozhodnout pořadí při shodě bodů? (podle zadání zatím neřešíme, řadíme podle přezdívky)
- Má jít turnaj ukončit dřív, než se odehrají všechna kola? (zatím jen zrušit)
