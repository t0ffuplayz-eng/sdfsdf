# mDowod

# mDowód - sprawdzanie poprawności cyfry kontrolnej
[TOC]

Metoda **`IsDocumentNumberCorrect`** w plikach `src/mDowod-validator.js` (javascript) oraz `DocumentNumberValidator.cs` (C#)

Sprawdza czy cyfra kontrolna numeru **Dowodu** / **mDowodu** / **Paszportu** / **Karty pobytu** jest poprawna.

Zgodna ze specyfikacjami ISO/IEC 7501-1:1997,
ICAO 9303    https://www.icao.int/Meetings/TAG-MRTD/TagMrtd22/TAG-MRTD-22_WP03-rev.pdf  strona 21
ICAO 9303-3  https://www.icao.int/publications/Documents/9303_p3_cons_en.pdf#search=9303%2D3 strona 19

## Algorytm liczenia cyfry kontrolnej mDowodu, Dododu, Paszportu, Karty pobytu

```
Literom przypisujemy liczby:
 ——————————————————————————————————————————————————————————————————————————————  
 |A ⁞B ⁞C ⁞ D⁞ E⁞ F⁞ G⁞ H⁞ I⁞ J⁞ K⁞ L⁞ M⁞ N⁞ O⁞ P⁞ Q⁞ R⁞ S⁞ T⁞ U⁞ V⁞ W⁞ X⁞ Y⁞ Z |
 |10⁞11⁞12⁞13⁞14⁞15⁞16⁞17⁞18⁞19⁞20⁞21⁞22⁞23⁞24⁞25⁞26⁞27⁞28⁞29⁞30⁞31⁞32⁞33⁞34⁞35 |
 —————————————————————————————————————————————————————————————————————————————— 
Algorytm obliczenia cyfry kontrolnej (CK) dla serii MAAA i numeru kolejnego 23456

  ———————————————————————————————————————————————————————————— 
 |Dane      ⁞  M     A    A    A  CK   2    3    4    5 ⁞     |
 |Wartości  ⁞  22   10   10   10  ▫▫   2    3    4    5 ⁞     |
 |Wagi      ⁞   7    3    1    7  ▫▫   3    1    7    3 ⁞     |
 |Iloczyny  ⁞ 154   30   10   70  ▫▫   6    3   28   15 ⁞     |
 |————————————————————————————————————————————————————————————|
 |Sumowanie ⁞ 154 + 30 + 10 + 70     + 6  + 3 + 28 + 15 = 316 |
  ———————————————————————————————————————————————————————————— 

 (22•7)+(10•3)+(10•1)+(10•7)+(2•3)+(3•1)+(4•7)+(5•3) = 316
 CK ↣ 316 MOD 10 = 6

Reszta z dzielenia 316 MOD 10 = 6  
Reszta z dzielenia stanowi cyfrę kontrolną i umieszczamy ją za literami serii
Otrzymujemy: MAAA 6 2345
```

## Uwagi

Autor wykonał testy dostępnych mu numerów mDowodu w dostępnej wersji aplikacji **mObywatel 2.0** w lipcu 2023.
