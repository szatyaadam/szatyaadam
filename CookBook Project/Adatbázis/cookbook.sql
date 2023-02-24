-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2023. Feb 04. 14:10
-- Kiszolgáló verziója: 10.4.27-MariaDB
-- PHP verzió: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `cookbook`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `favorites`
--

CREATE TABLE `favorites` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `MealsId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `favorites`
--

INSERT INTO `favorites` (`Id`, `UserId`, `MealsId`) VALUES
(13, 36, 111),
(14, 36, 112),
(15, 36, 113),
(16, 29, 140);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ingredients`
--

CREATE TABLE `ingredients` (
  `Id` int(11) NOT NULL,
  `MealId` int(11) DEFAULT NULL,
  `Quantity` int(11) NOT NULL,
  `MaterialsId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `ingredients`
--

INSERT INTO `ingredients` (`Id`, `MealId`, `Quantity`, `MaterialsId`) VALUES
(23, 11, 4, 4),
(47, 2, 3, 6),
(48, 2, 6, 3),
(50, 78, 10, 9),
(51, 78, 4, 10),
(52, 78, 10, 9),
(55, 111, 3, 18),
(58, 118, 4, 23),
(59, 118, 8, 24),
(60, 120, 1, 28),
(61, 120, 2, 29),
(62, 121, 4, 30),
(63, 121, 2, 31),
(64, 123, 4, 32),
(65, 125, 4, 34),
(66, 126, 4, 35),
(67, 127, 4, 35),
(68, 128, 4, 35),
(69, 129, 4, 35),
(70, 130, 5, 35),
(71, 133, 5, 35),
(74, 140, 2, 38),
(77, 144, 2, 37),
(78, 145, 2, 35),
(79, 147, 2, 39),
(80, 148, 2, 40),
(81, 149, 2, 39),
(82, 156, 2, 39),
(83, 157, 2, 39),
(84, 158, 5, 3),
(85, 159, 2, 39),
(86, 159, 1220, 44),
(87, 159, 10, 45),
(88, 159, 10, 45),
(89, 159, 10, 45),
(90, 159, 8, 46),
(91, 159, 4, 47);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `materials`
--

CREATE TABLE `materials` (
  `Id` int(11) NOT NULL,
  `IngredientName` varchar(50) NOT NULL,
  `UnitOfMeassuredId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `materials`
--

INSERT INTO `materials` (`Id`, `IngredientName`, `UnitOfMeassuredId`) VALUES
(1, 'Liszt', 3),
(2, 'Víz', 1),
(3, 'Csirke', 3),
(4, 'Só', 5),
(6, ' gffzgfguh', 2),
(9, 'hagyma', 3),
(10, 'kakaó', 2),
(13, 'vaj', 5),
(15, 'banán', 3),
(18, 'kurkuma', 2),
(22, 'hal', 2),
(23, 'kenyér', 2),
(24, 'morzsa', 1),
(26, 'ADSFSDFGS', 5),
(28, 'zsír', 1),
(29, 'olaj', 2),
(30, 'tejföl', 2),
(31, 'hús', 4),
(32, 'karifol', 2),
(34, 'marha', 2),
(35, 'marhaszegy', 2),
(37, 'marhaszegy', 3),
(38, 'marhaszegy', 3),
(39, 'lapocka', 2),
(40, 'lapocka', 3),
(44, 'Zeller', 5),
(45, 'Zeller', 2),
(46, 'Répa', 4),
(47, 'Répa', 2);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `meals`
--

CREATE TABLE `meals` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `MealName` varchar(100) NOT NULL,
  `PreperationTime` time DEFAULT NULL,
  `Price` float DEFAULT NULL,
  `Photo` varchar(160) DEFAULT NULL,
  `MealTypeId` int(11) DEFAULT NULL,
  `Discription` text DEFAULT NULL,
  `Privacy` tinyint(4) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `meals`
--

INSERT INTO `meals` (`Id`, `UserId`, `MealName`, `PreperationTime`, `Price`, `Photo`, `MealTypeId`, `Discription`, `Privacy`) VALUES
(2, 30, 'valami új készül', '00:15:10', 5000, 'asdfasdf', 3, 'Írok már vlaami leírást', 0),
(11, 30, 'asdasfasdgfdgadggad', '08:00:00', 10000, NULL, 3, 'fdasfdafegrtbgb ', 0),
(27, 36, 'borsó', '35:47:14', 4524, 'asdfasdf', 2, 'asdsadfsdf', 0),
(31, 36, 'borsóLeves', NULL, 423, 'bfbf', 1, NULL, 0),
(33, 36, 'sült kacsa', NULL, 1001100000, 'nincs', NULL, NULL, 0),
(54, 36, 'halászlé', NULL, 34234, 'nincs', 1, NULL, 1),
(55, 36, 'karbonara', NULL, 34234, 'nincs', NULL, NULL, 1),
(67, 36, 'leveskeésk', NULL, 1012, 'nincs', 2, NULL, 1),
(68, 36, 'newRECEPT', NULL, 1012, 'nincs', NULL, NULL, 0),
(69, 36, 'SzépAszzony', NULL, 10100, 'asdfasdf', NULL, 'Írok már vlaami leírást', 0),
(70, 36, 'fsakjfdkjsadégjaékgjaréfjklsdfésdkg', NULL, 10100, 'asdfasdf', NULL, 'Írok már vlaami leírást', 0),
(78, 35, 'borsostokány', '02:05:00', 56290, 'nem lehet url-t hazsnálni mert hosszú', 4, 'Az összetevöket összekeverjük.', 0),
(88, 36, 'valami új készül', NULL, 5000, 'asdfasdf', NULL, 'Írok már vlaami leírást', 0),
(89, 36, 'Ez már lehet jó lesz', NULL, 5000, 'asdfasdf', NULL, 'Írok már vlaami leírást', 0),
(103, 36, 'Rántott sajt', NULL, 5000, 'asdfasdf', NULL, 'Írok már vlaami leírást', 0),
(106, 36, 'Rántott karfiol', NULL, 430, 'Photo', NULL, 'Írok már vlaami leírást', 0),
(107, 36, 'Rántott karfiol', NULL, 430, 'Photo', NULL, 'Írok már vlaami leírást', 0),
(111, 36, 'Rántott karfiol', NULL, 430, 'Photo', 2, 'Írok már vlaami leírást', 0),
(112, 36, 'hajrá magyarok', NULL, 430, 'Photo', 2, 'Írok már vlaami leírást', 0),
(113, 36, 'hajrá magyarok', NULL, 430, 'Photo', 2, 'Írok már vlaami leírást', 0),
(114, 36, 'hajrá magyarok', NULL, 430, 'Photo', 2, 'Írok már vlaami leírást', 0),
(118, 36, 'koéadjffkldfffkddfgjlkdjgdkfgjélsdfkg', NULL, 43000, 'Photo', 1, 'Discription', 0),
(119, 36, 'koéadjffkldfffkddfgjlkdjgdkfgjélsdfkg', NULL, 43000, 'Photo', 1, 'Discription', 0),
(120, 36, 'koéadjffkldfffkddfgjlkdjgdkfgjélsdfkg', NULL, 43000, 'Photo', 1, 'Discription', 0),
(121, 36, 'newnenwnewn', NULL, 10000, 'Photo', 2, 'Discription', 0),
(123, 36, 'lkasjdlhglngrl', NULL, 10000, 'Photo', 2, 'Discription', 0),
(124, 36, 'lkasjdlhglngrl', NULL, 10000, 'Photo', 2, 'Discription', 0),
(125, 36, 'lkasjdlhglngrl', NULL, 10000, 'Photo', 2, 'Discription', 0),
(126, 36, 'lkasjdlhglngrl', NULL, 10000, 'Photo', 2, 'Discription', 0),
(127, 36, 'lkasjdlhglngrl', NULL, 10000, 'Photo', 2, 'Discription', 0),
(128, 36, 'lkasjdlhglngrl', NULL, 10000, 'Photo', 2, 'Discription', 0),
(129, 36, 'lkasjdlhglngrl', NULL, 10000, 'Photo', 2, 'Discription', 0),
(130, 36, 'almakompót', NULL, 123234, 'Photo', 2, 'Discription', 0),
(131, 36, 'almakompót', NULL, 123234, 'Photo', 2, 'Discription', 0),
(132, 36, 'almakompót', NULL, 123234, 'Photo', 2, 'Discription', 0),
(133, 36, 'almakompót', NULL, 123234, 'Photo', 2, 'Discription', 0),
(134, 36, 'almakompót', NULL, 123234, 'Photo', 2, 'Discription', 0),
(135, 36, 'almakompót', NULL, 123234, 'Photo', 2, 'Discription', 0),
(136, 36, 'almakompót', NULL, 123234, 'Photo', 2, 'Discription', 0),
(137, 36, 'almakompót', NULL, 123234, 'Photo', 2, 'Discription', 0),
(138, 36, 'almakompót', NULL, 123234, 'Photo', 2, 'Discription', 0),
(140, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(141, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(142, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(143, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(144, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(145, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(146, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(147, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(148, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(149, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(150, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(151, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(152, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(153, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(154, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(155, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(156, 36, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(157, 40, 'virslileves', NULL, 32123, 'Photo', 4, 'Discription', 0),
(158, 29, 'teszt', NULL, 10400, NULL, 4, 'TESZT', 0),
(159, 29, 'harakiri', '00:01:00', 10501, 'Photo', 4, 'Discription', 0),
(160, 29, 'harakiri2', '00:01:00', 23000, 'Photo', 4, 'Discription', 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `mealtypes`
--

CREATE TABLE `mealtypes` (
  `Id` int(11) NOT NULL,
  `MealTypeName` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `mealtypes`
--

INSERT INTO `mealtypes` (`Id`, `MealTypeName`) VALUES
(5, 'Desszert'),
(4, 'Főétel'),
(3, 'Főzelék'),
(2, 'Köret'),
(1, 'Leves'),
(15, 'Snack');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `meassures`
--

CREATE TABLE `meassures` (
  `Id` int(11) NOT NULL,
  `Measure` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `meassures`
--

INSERT INTO `meassures` (`Id`, `Measure`) VALUES
(5, 'Csipet'),
(2, 'Deciliter'),
(4, 'Gramm'),
(3, 'Kilógramm'),
(1, 'Liter'),
(6, 'Teáskanál');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `roles`
--

CREATE TABLE `roles` (
  `Id` int(11) NOT NULL,
  `RoleName` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `roles`
--

INSERT INTO `roles` (`Id`, `RoleName`) VALUES
(1, 'Admin'),
(2, 'Editor'),
(3, 'User');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `tokens`
--

CREATE TABLE `tokens` (
  `Id` int(11) NOT NULL,
  `Token` varchar(255) NOT NULL,
  `Exp_date` datetime NOT NULL,
  `UserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `tokens`
--

INSERT INTO `tokens` (`Id`, `Token`, `Exp_date`, `UserId`) VALUES
(22, '+b2zDOTcZfqta06xzotxP6ICf3ggQLuoQLotoId9NQo=', '2023-01-31 16:52:16', 35),
(25, 'aq5j5zZSG3VOA2CVmk8wM15wMU1+OAimgPkqP3Un9XA=', '2023-02-01 15:26:11', 30),
(58, 'EB+UfeOH8I+KvuuAjJi+jOlQ1JW8WDP7O9YDLN+QXKE=', '2023-02-07 15:52:45', 40),
(59, 'vnLPuBSw/waS3VEAkKCGWc0TxAjyNZ4BfaHpyIYmhSw=', '2023-02-07 16:03:39', 33),
(60, '2Jkfy4QvWLXgSiC0FaqHIUESsvx75+EiN8J7pxGSPEQ=', '2023-02-08 15:48:49', 36),
(69, '3pM1dofEbctun8fwv5gMM82slcwiEnk4YpVq4P7XvPc=', '2023-02-10 19:43:18', 43),
(71, '5txpbrOvejXyZrUBu6ktCT6FbL1h94tNr2uJnqo6VuE=', '2023-02-11 08:33:11', 45),
(74, 'kke0m6/troXV6bzISwg07Ze4kCNltaC2DDeuKMHgXes=', '2023-02-11 13:44:16', 29);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `Id` int(11) NOT NULL,
  `UserName` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Password` varchar(60) NOT NULL,
  `RoleId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`Id`, `UserName`, `Email`, `Password`, `RoleId`) VALUES
(29, 'Béla', 'Béla', '$2a$11$NCVJ3EMiW3QAllmQ1RjVe.x/dft4T1oPztLsIgjPj4ai5W8RpikoO', 3),
(30, 'Ann', 'Anna', '$2a$11$acSJ3daOnlmjOACFhcIUz.8s3OedGGK/cfYi08PClLMPIO3Q7u0Ly', 1),
(32, 'Ivory0', 'ivory0@gmail.com', '$2a$11$S/t4jli72sa/IUhujPYzBOsCJhu8Hyi.JiiUvg9PgRlQhtNN/f8ly', 3),
(33, 'admin100', 'admin100', '$2a$11$baWYDYq8R0OdzZHf3IrJf.cXND6Jm3wGj0kgccckVW83b9sID12o.', 1),
(35, 'gabi', 'gabivagyok', '$2a$11$LEijHyFfcHv6u4Vl8GiwXeetumBVysrSa/HPHBZjjYxjjm7POHrIa', 2),
(36, 'hali', 'asd', '$2a$11$IZ4fFONT2R3OUvNDMEF25eFesFnwQGNiS6ONRNV5xA3Mb2nphxdT6', 3),
(37, 'megváltoztattam', 'user', '$2a$11$S43QoiKG3xMLAeVtgzk4PuYMVLqNmgcbgYnUYea1GoM2B5PhjHGfy', 3),
(38, 'user20', 'user20', '$2a$11$gzoA/e707NgB4H01u2P2mOlm7470cy6l..1rIzouyWiX917jD98z6', 3),
(39, 'user200', 'user200', '$2a$11$jlhVPy2mGcvPv6MbpLiXr.tTefsOWuzrYiqvxg9WFOfjIsu9jHKU2', 3),
(40, '12345', '12345', '$2a$11$arGMz4WENjwQiBm/vIWhauGEmFRZfTjC7T/hn/sVDFvf1UfUOjU92', 3),
(42, 'fullNewUser', '123456', '$2a$11$74mVFX34DF6EF8QNHBT.D.1ZNHZIew7mw.Kpte6.VF3P49b/Esua2', 3),
(43, 'adminvagyok', 'adminvagyok', '$2a$11$Uly/ExDiBdA8r3bmhk3UPO1QqQh3dVWey6/dVFkPiO0HeVLMnTTSu', 3),
(44, 'ivory', 'ivory', '$2a$11$/pswF/7aRJhQIBhgsb8O6eG30yXqZBHjmp.oikT3FNGo3ZUha1Hi6', 3),
(45, 'admin', 'admin', '$2a$11$rF7Wp.i/ugbxJz5XKrveuOIEchOeMqUO/wOWcdPV3hsN0JssY0bDC', 3),
(46, '', 'ivo', '$2a$11$9zCbxBnFg.7HNuxYV4Gso.O0LECuVLr/41NX540VShFscM9aalH6u', 3);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `favorites`
--
ALTER TABLE `favorites`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `UserId` (`UserId`),
  ADD KEY `MealsId` (`MealsId`);

--
-- A tábla indexei `ingredients`
--
ALTER TABLE `ingredients`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `ingredients_ibfk_2` (`MaterialsId`),
  ADD KEY `MealId` (`MealId`) USING BTREE;

--
-- A tábla indexei `materials`
--
ALTER TABLE `materials`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `UnitOfMeassuredId` (`UnitOfMeassuredId`),
  ADD KEY `IngredientName` (`IngredientName`) USING BTREE;

--
-- A tábla indexei `meals`
--
ALTER TABLE `meals`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `UserId` (`UserId`),
  ADD KEY `MealTypeId` (`MealTypeId`) USING BTREE;

--
-- A tábla indexei `mealtypes`
--
ALTER TABLE `mealtypes`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `MealTypeName` (`MealTypeName`);

--
-- A tábla indexei `meassures`
--
ALTER TABLE `meassures`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Measure` (`Measure`);

--
-- A tábla indexei `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `tokens`
--
ALTER TABLE `tokens`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `UserId` (`UserId`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UserName` (`UserName`),
  ADD UNIQUE KEY `Email` (`Email`),
  ADD KEY `users_ibfk_1` (`RoleId`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `favorites`
--
ALTER TABLE `favorites`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT a táblához `ingredients`
--
ALTER TABLE `ingredients`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=92;

--
-- AUTO_INCREMENT a táblához `materials`
--
ALTER TABLE `materials`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=48;

--
-- AUTO_INCREMENT a táblához `meals`
--
ALTER TABLE `meals`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=161;

--
-- AUTO_INCREMENT a táblához `mealtypes`
--
ALTER TABLE `mealtypes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT a táblához `meassures`
--
ALTER TABLE `meassures`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT a táblához `roles`
--
ALTER TABLE `roles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `tokens`
--
ALTER TABLE `tokens`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=75;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=47;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `favorites`
--
ALTER TABLE `favorites`
  ADD CONSTRAINT `favorites_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `ingredients`
--
ALTER TABLE `ingredients`
  ADD CONSTRAINT `ingredients_ibfk_1` FOREIGN KEY (`MealId`) REFERENCES `meals` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `ingredients_ibfk_2` FOREIGN KEY (`MaterialsId`) REFERENCES `materials` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `materials`
--
ALTER TABLE `materials`
  ADD CONSTRAINT `materials_ibfk_1` FOREIGN KEY (`UnitOfMeassuredId`) REFERENCES `meassures` (`Id`);

--
-- Megkötések a táblához `meals`
--
ALTER TABLE `meals`
  ADD CONSTRAINT `meals_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `meals_ibfk_2` FOREIGN KEY (`MealTypeId`) REFERENCES `mealtypes` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Megkötések a táblához `tokens`
--
ALTER TABLE `tokens`
  ADD CONSTRAINT `tokens_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
