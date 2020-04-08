SELECT
     Nume, CalendarDate
FROM
     Planificari
     INNER JOIN Localitati ON Planificari.IdLocalitate = Localitati.IdLocalitate
     JOIN Calendar c ON c.CalendarDate BETWEEN Planificari.DataStart AND Planificari.DataStop 
WHERE DataStart
    BETWEEN '19.12.2019' AND '27.06.2020'
ORDER BY
    Nume, CalendarDate;
