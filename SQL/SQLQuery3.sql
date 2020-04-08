SET DATEFORMAT dmy
SELECT
     Nume, CalendarDate, CaleFisier
FROM
     Planificari
     INNER JOIN Localitati ON Planificari.IdLocalitate = Localitati.IdLocalitate
     INNER JOIN Imagini ON Planificari.IdLocalitate = Imagini.IdLocalitate
     JOIN Calendar c ON c.CalendarDate BETWEEN Planificari.DataStart AND Planificari.DataStop 
WHERE DataStart
    BETWEEN '19.12.2019' AND '27.06.2020'
GROUP BY Nume, CalendarDate, CaleFisier
ORDER BY Nume, CalendarDate;
