SET DATEFORMAT dmy
use master
go
if exists (select * from sys.databases where name='Turism')
	begin
		alter database Turism set single_user 
			with rollback immediate
		drop database Turism
	end
go
create database Turism
go
alter authorization on database::Turism to sa
go
use Turism
go
create table Localitati
(
	IdLocalitate int primary key -- Cheie primară ce identifică o localitate
	,Nume nvarchar(100) UNIQUE -- Numele localității
)
create table Imagini
( 
	IdImagine int primary key -- Cheie primară ce identifică o imagine
	,IdLocalitate int foreign key (IdLocalitate) references Localitati -- Identifică localitatea căreia îi aparține imaginea
	,CaleFisier nvarchar(max) -- Reține calea (path) fișierului de tip imagine
)
create table Planificari
(
    IdVizita int primary key -- Cheie primară ce identifică o vizită
    ,IdLocalitate int UNIQUE foreign key (IdLocalitate) references Localitati -- Identifică localitatea de vizitat
    ,DataStart datetime -- Data calendaristică în care începe vizitarea localității, în cazul planificărilor ocazionale
    ,DataStop datetime -- Data calendaristică în care se încheie vizitarea localității, în cazul planificărilor ocazionale
)
create table Users
(
    IdUsername int IDENTITY(1,1) primary key -- Cheie primară ce identifică un utilizator
    ,Username nvarchar(20) UNIQUE 
    ,Password nvarchar(10)
)
GO
--Inserarea datelor
INSERT INTO Localitati (IdLocalitate, Nume) VALUES 
	(1, N'Târgu Neamţ')
    ,(2, 'Cluj-Napoca')
    ,(3, N'Ploiești')
    ,(4, 'Sinaia')
    ,(5, N'Constanța')
    ,(6, 'Mangalia')
    ,(7, 'Vatra Dornei')
    ,(8, N'Iași')
    ,(9, 'Sibiu')
    ,(10, 'Predeal')
    ,(11, N'Bârsana')
    ,(12, N'Borșa')
    ,(13, N'Brașov')
GO
INSERT INTO Imagini (IdImagine, IdLocalitate, CaleFisier) VALUES 
	(1, 1, 'TarguNeamt1')
    ,(2, 1, 'TarguNeamt2')
    ,(3, 1, 'TarguNeamt3')
    ,(4, 2, 'Cluj1')
    ,(5, 2, 'Cluj2')
    ,(6, 2, 'Cluj3')
    ,(7, 2, 'Cluj4')
    ,(8, 3, 'Ploiesti1')
    ,(9, 3, 'Ploiesti2')
    ,(10, 3, 'Ploiesti3')
    ,(11, 3, 'Ploiesti4')
    ,(12, 4, 'Sinaia1')
    ,(13, 4, 'Sinaia2')
    ,(14, 4, 'Sinaia3')
    ,(15, 5, 'Constanta1')
    ,(16, 5, 'Constanta2')
    ,(17, 5, 'Constanta3')
    ,(18, 5, 'Constanta4')
    ,(19, 6, 'Mangalia1')
    ,(20, 6, 'Mangalia2')
    ,(21, 7, 'VatraDornei1')
    ,(22, 7, 'VatraDornei2')
    ,(23, 8, 'Iasi1')
    ,(24, 8, 'Iasi2')
    ,(25, 8, 'Iasi3')
    ,(26, 8, 'Iasi4')
    ,(27, 9, 'Sibiu1')
    ,(28, 9, 'Sibiu2')
    ,(29, 9, 'Sibiu3')
    ,(30, 9, 'Sibiu4')
    ,(31, 10, 'Predeal1')
    ,(32, 10, 'Predeal2')
    ,(33, 10, 'Predeal3')
    ,(34, 11, 'Barsana1')
    ,(35, 11, 'Barsana2')
    ,(36, 11, 'Barsana3')
    ,(37, 11, 'Barsana4')
    ,(38, 11, 'Barsana5')
    ,(39, 12, 'Borsa1')
    ,(40, 12, 'Borsa2')
    ,(41, 12, 'Borsa3')
    ,(42, 12, 'Borsa4')
    ,(43, 13, 'Brasov1')
    ,(44, 13, 'Brasov2')
    ,(45, 13, 'Brasov3')
    ,(46, 13, 'Brasov4')
    ,(47, 13, 'Brasov5')
    ,(48, 13, 'Brasov6')
    ,(49, 13, 'Brasov7')
GO
INSERT INTO Planificari (IdVizita, IdLocalitate, DataStart, DataStop) VALUES 
    (1, 1, '02.05.2019', '04.05.2019')
    ,(2, 2, '10.05.2019', '15.05.2019')
    ,(3, 3, '18.05.2019', '21.05.2019')
    ,(4, 4, '21.05.2019', '23.05.2019')
    ,(5, 5, '12.06.2019', '15.06.2019')
    ,(6, 6, '13.06.2019', '18.06.2019')
    ,(7, 7, '28.07.2019', '05.08.2019')
	,(8, 8, '15.08.2019', '20.08.2019')
    ,(9, 9, '01.09.2019', '10.09.2019')
    ,(10, 10, '19.09.2019', '29.09.2019')
    ,(11, 11, '10.01.2020', '18.01.2020')
    ,(12, 12, '29.01.2020', '07.02.2020')
    ,(13, 13, '19.02.2020', '28.02.2020')
GO
INSERT INTO Users (Username, Password) VALUES 
    ('admin', 'admin')
	,('baciuness', 'portugal')
GO