-- Interogari simple : implementate
-- interogare 1 : Care este nr de jucatori dintr-o echipa?
--SELECT E.Nume, COUNT(*) AS Numar_jucatori FROM Jucatori J INNER JOIN Echipe E ON J.ID_Ech = E.ID_Ech
--WHERE E.ID_Ech = 2
--GROUP BY E.Nume;

--interogare 2 : Care sunt numele echipelor care au jucat un meci?
--SELECT M.ID_Meci, E1.Nume, E2.Nume
--FROM Meciuri M INNER JOIN Echipe E1 ON M.ID_Ech1 = E1.ID_Ech
--INNER JOIN Echipe E2 ON M.ID_Ech2 = E2.ID_Ech
--WHERE M.ID_Meci = 7;

--(poate fi orice ID, al oricrei echipe)

--interogare 3 : Cate goluri a marcat un jucator intr-un meci?
--SELECT J.Nume + ' ' + J.Prenume AS Jucator, M.ID_Meci, COUNT(*) AS 'Num¾r de goluri' FROM Goluri G
--INNER JOIN Jucatori J ON G.ID_Juc = J.ID_Juc
--INNER JOIN Meciuri M ON G.ID_Meci = M.ID_Meci
--WHERE M.ID_Meci = 4 --(poate fi orice ID, al oric¾rui meci)
--GROUP BY J.Nume, J.Prenume, J.ID_Juc, M.ID_Meci;

--interogare 4 : Care sunt jucatorii care joaca pe pozitia aleasa de user?
--SELECT J.Nume + ' ' + Prenume AS Jucator, Data_N AS 'Data nasterii', E.Nume AS Echipa, J.Pozitie, Capitan FROM Jucatori J, Echipe E
--WHERE J.Pozitie = 'CB' AND J.ID_Ech = E.ID_Ech;

--interogare 5 : Stadioanele pe care s-au marcat cel putin de n goluri
--SELECT S.Denumire, S.Locatie, S.Capacitate, S.Cost, COUNT(ID_Gol) AS Goluri_marcate FROM Meciuri M
--INNER JOIN Stadioane S ON M.ID_Std = S.ID_Std
--INNER JOIN Goluri G ON G.ID_Meci = M.ID_Meci
--GROUP BY S.Denumire, S.Locatie, S.Capacitate, S.Cost
--HAVING COUNT(ID_Gol) >= 3;

--interogare 6 : Capitanii care au dat goluri si in ce meci
--SELECT DISTINCT J.Nume, J.Prenume, J.Capitan, G.ID_Meci AS Meci, COUNT(*) AS Goluri_marcate FROM Jucatori J, Goluri G
--WHERE G.ID_Juc = J.ID_Juc AND J.Capitan = 'da'
--GROUP BY J.Nume, J.Prenume, J.Capitan, G.ID_Meci 
--ORDER BY J.Nume

----------------------------------------------------------------------------------------


--Interogari complexe
--interogare 1 : Care sunt stadioanele pe care s-au jucat meciurile din data aleasa de user? -- implementat
--SELECT M.Data, S.Denumire AS Nume_Stadion, E1.Nume AS 'Echipa 1', E2.Nume AS 'Echipa 2', M.Scor_Ech1, M.Scor_Ech2
--FROM Meciuri M, Stadioane S, Echipe E1, Echipe E2
--WHERE M.ID_Ech1 = E1.ID_Ech AND M.ID_Ech2 = E2.ID_Ech AND M.ID_Std = S.ID_Std 
--AND M.ID_Meci IN
--	(
--	SELECT ID_Meci FROM Meciuri
--	WHERE Data = '2018-03-15'
--	)
--GROUP BY M.Data, S.Denumire, E1.Nume, E2.Nume, M.Scor_Ech1, M.Scor_Ech2;

-- interogare 2 : Jucatorii ai caror echipe au jucat mai mult de n meciuri - implementat
--SELECT J.Nume, Prenume, Data_N, E.Nume, J.Pozitie FROM Jucatori J, Echipe E
--WHERE EXISTS
--(SELECT J.ID_Ech FROM Meciuri M
--WHERE J.ID_Ech = M.ID_Ech1 OR 
--J.ID_Ech = M.ID_Ech2
--HAVING COUNT(*) > 2) AND J.ID_Ech = E.ID_Ech
--GROUP BY J.Nume, Prenume, Data_N, E.Nume, J.Pozitie;

--interogare 3: Cate goluri a jucat fiecare jucator, sortati dupa meciuri jucate
--SELECT J.Nume + ' ' + Prenume AS 'Nume jucator',
--(SELECT COUNT(*) FROM Goluri WHERE Goluri.ID_Juc = J.ID_Juc) AS 'Goluri marcate',J.Pozitie, Data_N AS 'Data nasterii',
--E.Nume AS 'Echipa' 
--FROM Jucatori J, Echipe E
--WHERE J.ID_Ech = E.ID_Ech
--ORDER BY (SELECT COUNT(*) FROM Meciuri WHERE J.ID_Ech = ID_Ech1 OR J.ID_Ech = ID_Ech2) DESC

-- interogare 4: Care sunt primii 5 antrenori cu cel mai mare salariu? - implementat
-- nu e ok cu top -> trunchiaza rezultatele
SELECT A.Nume + ' ' +  A.Prenume AS 'Nume antrenor', A.[Data nasteri], A.Salariu
FROM (SELECT TOP 10 A.Salariu AS Salariu, A.Nume AS Nume, A.Prenume AS Prenume, 
A.Data_N AS 'Data nasteri' FROM Antrenori A
ORDER BY A.Salariu) AS A


-- Jucatorii care au dat gol in fiecare meci al echipelor lor
--SELECT Nume, Prenume FROM Jucatori
--WHERE (
--	SELECT ID_Meci 
--	FROM Goluri 
--	WHERE Jucatori.ID_Juc = ID_Juc
--	) 
--	CONTAINS
--	(
--	SELECT ID_Meci FROM Meciuri
--	WHERE Jucatori.ID_Ech = ID_Ech1 OR Jucatori.ID_Ech = ID_Ech2
--	)
