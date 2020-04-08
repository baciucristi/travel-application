SET DATEFORMAT dmy
SELECT Nume, DataStart, DataStop FROM Planificari
INNER JOIN Localitati ON Planificari.IdLocalitate = Localitati.IdLocalitate
WHERE DataStart
BETWEEN '19.05.2017' AND '27.06.2017'

