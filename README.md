# Pumpa za struju lol

### Specifikacija

Podrzavaju se sve snage, marke (univerzalan punjac)   

Korisnici: Admin, Klijent, Menadzer.   

## Menadzer 

- Menadzer moze da vidi istoriju za sve korisnike. On nadgleda parking.  

## Admin 

- Treba da ima izvestaj u realnom vremenu i periodicno (za proslu nedelju, mesec...).   
- Izvestaj za administratora: trenutno stanje, popunjenost, prosecna popunjenost po vremenu i po stanici, realtime da li rade ili ne rade punjaci.   

## Klijent 

- Klijenti moraju da se loguju.  
- Klijent moze za odredjenu sumu da dopuni automobil.  
- Klijent moze da dodaje koje automobile poseduje (snaga punjaca).   
- Klijent moze da vidi istoriju punjenja sa vremenskim slotovima, koliko energije i kolika cena je vazila tad (izvestaj).   
- Klijent moze da vidi koliko je placao.   
- Klijent moze da vidi svoju istoriju.   
- O klijentu se pamte podaci iz licne karte.   
- Klijent moze da bude pravno ili fizicko lice. Fizickim licima se cena racuna na licu mesta. Za pravna lica postoje pogodnosti ili mogu 
da plate kasnije.   
- Za klijenta se pamti stanje (prepaid dopuna)

## RFID Kartica

- Kartica mora da se mahne pa se otvori poklopac.   
- Prepaid klijent dobija pogodnosti.   
- 1 automobil je jedna kartica, a 1 korisnik moze imati vise kartica.    

## Cena

- Cena se modeluje kao klasican cenovnik (datum vaznja, po kwh).   
- Sto duze puni -> cena veca (zbog snage).    
- Na cenu utice i guzva (na osnovu rezervacija).   
- Cena je base cena (dnevna/nocna) a posle su sve koeficijenti.   
- Cena zavisi i od lokacije pumpe. Jeftina struja je faktor (doba dana je bitno).   
- Najvece poskupljenje je 1.25 koeficijent.   

## Rezervacija

- Unapred se racuna cena kada rezervise.   
- Najkasnije moze mesec dana unapred da se rezervise.   
- Slotovi za zakazivanje su na 15 minuta (slot moze da se zakaze 4 puta u satu).   
- Rezervacije za mesto, ne za pumpu.   
- Odredjeni procenat punjaca ne sme da bude rezervisan (ne sme sve da bude rezervisano).   
- Postoje penali za prekovremeno ostajanje na parking mestu i za nedolazak u rezervisanom terminu.   
- Kada klijent zakaze, preracunava se koliko ce ostati (u zavisnosti od snage).   
- Ako ne dodje na rezervaciju, dolazi mu obavestenje.   
- Otkazivanje do 15 min pre pocetka slota. Ako ne dodje, ceka se 15 minuta pa se slot oslobadja.   

## Punjac

- OCPP protokol.   
- Punjaci treba da se javljaju da li su zivi (periodicno).   
- Zavisno od automobila zavisi i trajanje punjenja.   
- Jedan punjac je 1 ili 2 parking mesta.   
- Valjalo bi da se ne javljaju svi u istom trenutku jer ne zelimo da DDOSujemo sami sebe.  

## Kvar

- Ako se desi kvar, dajemo mu sledecu slobodnu pumpu ako postoji, ako ne postoji, dobija besplatno punjenje.   
- Ako se pokvari pumpa pre nego sto dodje, stize notifikacija i prebacuje se na drugo mesto.   

## Opste
- Bekap svih podataka.   
- U zavisnosti od GPS lokacije, recommenduje se najbliza pumpa.   
- Nivoi snage - 250, 100, 55, 22.   

Kraj :)  
