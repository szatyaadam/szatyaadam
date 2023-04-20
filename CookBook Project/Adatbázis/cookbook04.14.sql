-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2023. Ápr 14. 11:25
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
CREATE DATABASE IF NOT EXISTS `cookbook` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE `cookbook`;

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
(32, 48, 161),
(33, 48, 162);

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
(92, 161, 4, 48),
(93, 161, 3, 49),
(94, 161, 2, 50),
(95, 161, 1, 51),
(96, 161, 1, 52),
(97, 161, 1, 53),
(98, 162, 2, 48),
(99, 162, 1, 52),
(100, 162, 1, 53),
(101, 162, 30, 54),
(102, 162, 1, 55),
(103, 162, 5, 56),
(104, 162, 3, 57),
(105, 162, 2, 58),
(106, 162, 1, 59),
(107, 162, 1, 60),
(108, 163, 10, 56),
(109, 163, 1, 52),
(110, 163, 1, 53),
(111, 163, 80, 61),
(112, 163, 2, 62),
(113, 163, 1, 63),
(114, 163, 1, 64),
(115, 163, 1, 65),
(116, 163, 4, 66),
(117, 163, 25, 67),
(118, 163, 5, 68),
(119, 163, 1, 69),
(120, 163, 1, 70),
(121, 163, 1, 71),
(122, 164, 25, 56),
(123, 164, 30, 62),
(124, 164, 5, 56),
(125, 164, 2, 72),
(126, 164, 1, 73),
(127, 164, 2, 74),
(128, 164, 50, 75),
(129, 164, 2, 76),
(130, 164, 50, 77),
(131, 164, 8, 78),
(132, 164, 2, 79),
(133, 164, 8, 80),
(134, 164, 5, 81),
(135, 165, 1, 52),
(136, 165, 10, 82),
(137, 165, 3, 83),
(138, 165, 2, 84),
(139, 165, 50, 85),
(140, 165, 1, 86),
(141, 165, 35, 87),
(142, 165, 50, 88),
(143, 165, 50, 89),
(144, 165, 10, 90),
(145, 165, 1, 91),
(146, 165, 1, 92),
(149, 168, 2121, 95);

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
(48, 'tojás', 22),
(49, 'mustár', 12),
(50, 'vaj', 12),
(51, 'snidling', 17),
(52, 'só', 18),
(53, 'bors', 18),
(54, 'véreshurka', 6),
(55, 'étolaj', 2),
(56, 'vaj', 6),
(57, 'csicsóka', 22),
(58, 'kaffir lime levél', 22),
(59, 'édeskömény', 22),
(60, 'gyümölcsecet', 2),
(61, 'báránygerinc', 6),
(62, 'cukor', 6),
(63, 'karalábé', 22),
(64, 'citrom', 22),
(65, 'fehérbor', 2),
(66, 'mentával infúzionált kakaóbabhéj', 6),
(67, 'tejszín', 2),
(68, 'kakukkfű', 22),
(69, 'friss petrezselyem', 22),
(70, 'olívaolaj', 2),
(71, 'babcsíra', 17),
(72, 'tej', 2),
(73, 'cukor', 10),
(74, 'élesztő', 6),
(75, 'liszt', 6),
(76, 'tojássárgája', 22),
(77, 'dióbél', 6),
(78, 'sárgabaracklekvár', 2),
(79, 'tej', 2),
(80, 'porcukor', 6),
(81, 'kakaópor', 6),
(82, 'olajban eltett aszalt paradicsom', 6),
(83, 'fokhagyma', 22),
(84, 'olívaolaj', 12),
(85, 'csirkemellfilé', 6),
(86, 'édes-nemes paprika', 18),
(87, 'penne', 6),
(88, 'tej', 4),
(89, 'cukrozatlan habtejszín', 4),
(90, 'mozzarella + a tálaláshoz', 6),
(91, 'bazsalikom + a tálaláshoz', 17),
(92, 'csilipehely', 18),
(93, 'só', 12),
(94, 'test', 12),
(95, 'kávé', 13),
(96, 'liszt', 12);

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
  `MealTypeId` int(11) NOT NULL,
  `Discription` text DEFAULT NULL,
  `Privacy` tinyint(4) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `meals`
--

INSERT INTO `meals` (`Id`, `UserId`, `MealName`, `PreperationTime`, `Price`, `Photo`, `MealTypeId`, `Discription`, `Privacy`) VALUES
(161, 47, 'SNIDLINGES TOJÁSKRÉM - AHOGY ANYUKÁM CSINÁLTA', '00:15:00', 1500, 'https://www.mindmegette.hu/images/378/L/lead_L_koret-3.jpg', 32, 'A snidlinges tojáskrém elkészítéséhez a tojásokat 10-12 perc alatt keményre főzzük, négybe vágjuk, majd a sárgájukat villával összetörjük, elkeverjük a mustárral, a vajjal és a sóval.\nA fehérjét belereszeljük - vagy nagyon apróra vágva beletesszük - a masszába, megszórjuk az apróra vágott snidlinggel, és jól elkeverjük.\nMindenki kedvére szórhat bele mindenféle zöldfűszert, egy kis borsot, némi pirospaprikát. A keveréket pár percig pihentetjük, majd vastagon megkenjük vele a kenyerünket.', 0),
(162, 47, 'FŰSZERES VÉRES HURKA', '00:30:00', 3800, 'https://www.mindmegette.hu/images/377/L/lead_L_troll_a_konyhaban_hurka.jpg', 17, 'A sütőt 180 fokra előmelegítjük, majd körülbelül 15 perc alatt megsütjük benne a hurkát.\nAmíg a hús elkészül, a meghámozott, apró kockákra vágott csicsókát kevés olajon aranybarnára pirítjuk.\nAz édesköményt vékony szeletekre vágjuk összeforgatjuk a gyümölcsecettel, majd a mézzel, utána pedig sózzuk, borsozzuk.\nTálaláskor szépen a hurka mellé helyezzük a csicsókát, az édeskömény-salátát, majd egy mustárcsíkkal és egy tükörtojással díszítjük.', 0),
(163, 48, 'KARALÁBÉS BÁRÁNYBORDA WOSSALA ROZINA MEGLEPETÉS HOZZÁVALÓJÁVAL', '01:15:00', 5500, 'https://www.mindmegette.hu/images/375/XL/lead_XL_karalabes_baranyborda_foto.jpg', 17, 'A megtisztított karalábét hasábokra vágjuk és enyhén sós vízben megfőzzük. Ha megpuhult, leszűrjük, lecsöpögtetjük, majd forró serpenyőben kevés olívaolajon elkezdjük pirítani. Sózzuk, borsozzuk, és miután megpirult, ráöntjük a bort és a citrom levét. Ahogy elforr a bor, levesszük a tűzről és megszórjuk a finomra vágott petrezselyemmel, majd tálalásig félretesszük.\nA báránybordát megtisztítjuk, leeső részeiből pedig alaplevet főzünk. Ízesítjük fehérborral, mentával, az infúzionált kakaóbabbal, sóval és borssal. Fél óra főzés után leszűrjük, és tejszínnel, vajjal mártásállagúra forraljuk.\nA bárányt sózzuk, borsozzuk, majd forró serpenyőben, vajjal locsolgatva mindkét oldalát megsütjük 3-4 perc alatt, közben friss kakukkfüvet dobunk rá. 2-3 perc pihentetés után tálaljuk.', 0),
(164, 48, 'HAGYOMÁNYOS ZSERBÓ', '01:00:00', 4500, 'https://www.mindmegette.hu/images/366/L/lead_L_zserbo.jpg', 16, 'A hagyományos zserbó tésztájához a tejet langyosítsd meg, rakd hozzá a cukrot, és morzsold hozzá az élesztőt. Hagyd felfutni, tehát várj körülbelül 10 percig, amíg jól megemelkedik, és a teteje habos lesz. A lisztet szitáld egy tálba, add hozzá a tojássárgáját, a vajat és a megkelt élesztőt. Jól gyúrd össze, majd hideg helyen pihentesd egy órát.\nA töltelékhez a diót darált le, és keverd össze a cukorral. Pihentetés után a tésztát oszd három részre, és lisztezett felületen mindegyiket nyújtsd ki. Az egyik nyers lapot igazgasd magas szélű tepsibe, kend meg lekvárral, és szórd meg a cukrozott dióval. Helyezd rá a következő lapot, és azzal is hasonlóan járj el, mint az elsővel. A tetejére igazítsd az utolsó lapot, és villával szurkáld meg több helyen. Told forró sütőbe, és 170 fokon süsd meg körülbelül 35 perc alatt aranybarnára. Arányosan növelve a hozzávalókat több lap süthető!\nA mázhoz öntsd egy tűzálló edénybe a tejet, add hozzá a cukrot és a kakaót, majd folyamatosan kevergetve forrald fel. Amikor szép fényes, húzd le a tűzről, és keverd hozzá a vajat. Széles késsel gyorsan kend a tészta tetejére. Hagyd kihűlni, majd tálalás előtt szeleteld fel.\n\nJó tanács: a sütés befejezése után a tepsiből kifordítva hagyd úgy a tésztát. Vagyis ami a tepsi aljával érintkezett, az kerül felülre. Így szép sima egyenletes felületű lesz a sütemény! ', 0),
(165, 48, 'BAZSALIKOMOS-PARADICSOMOS CSIRKÉS TÉSZTA', '00:45:00', 4200, '', 29, 'Száraz serpenyőben kissé megpirítjuk a lecsepegtetett, felaprított aszalt paradicsomot a meghámozott, átnyomott fokhagymával együtt. Azután kiszedjük, és félretesszük.\nAz olívaolajat a serpenyőbe locsoljuk, felhevítjük, és megpirítjuk rajta a megtisztított, felcsíkozott csirkemellet. Meghintjük egy kevés sóval és 1 csipet pirospaprikával.\nA pennét forrásban lévő, sós vízben a csomagolásán megadott utasítás szerint megfőzzük. Félreteszünk kb. 50 ml főzővizet, majd leszűrjük és lecsepegtetjük a tésztát.\nA húscsíkokhoz keverjük az aszalt paradicsomos keveréket, a tejet, a tejszínt, a felkockázott sajtot, valamint a félretett főzővizet, ezután forgassuk bele a tésztát. Végül a bazsalikom lecsipkedett, felcsíkozott leveleivel és némi csilipehellyel ízesízjük. Forrón, néhány bazsalikomlevéllel díszítve, továbbá egy kevés reszelt sajttal megszórva tálaljuk.', 0),
(168, 48, 'teszt', '10:40:00', 3030, '', 16, 'teszt', 0);

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
(16, 'desszert'),
(17, 'főétel'),
(18, 'főzelék'),
(19, 'hal'),
(20, 'hús'),
(21, 'húsvét'),
(22, 'karácsony'),
(23, 'köret'),
(24, 'leves'),
(25, 'reggeli'),
(26, 'saláta'),
(33, 'snack'),
(27, 'szósz'),
(28, 'tengeri'),
(29, 'tészta'),
(30, 'torta'),
(31, 'vacsora'),
(32, 'vegeráriánus');

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
(15, 'bögre'),
(3, 'centiliter'),
(19, 'csapott evőkanál'),
(14, 'csésze'),
(18, 'csipet'),
(17, 'csomag'),
(22, 'darab'),
(2, 'deciliter'),
(6, 'dekagramm'),
(12, 'evőkanál'),
(16, 'gerezd'),
(7, 'gramm'),
(11, 'gyermekkanál'),
(8, 'kávéskanál'),
(21, 'késhegynyi'),
(5, 'kilogramm'),
(1, 'liter'),
(4, 'milliliter'),
(9, 'mokkáskanál'),
(13, 'pohár'),
(20, 'púpozott evőkanál'),
(10, 'teáskanál');

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
(319, 'Gs6+PR64NC3PXBVs71zq4eK5sLDgig1op53+k+play4=', '2023-04-21 11:24:04', 48);

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
(47, 'admin123', 'admin@gmail.com', '$2a$11$HVLJD6dLYkbQhT4KNyHfWe.0p7aLOCOESrZkROvqXP0HfoFfFJRpC', 3),
(48, 'SummerDev', 'summerdev@gmail.com', '$2a$11$D7B37p.QTaJJsw7xW6Xpmu9s7jDjpkATFvJoVCMPoVE6HTVQcs2A.', 3);

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
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- AUTO_INCREMENT a táblához `ingredients`
--
ALTER TABLE `ingredients`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=152;

--
-- AUTO_INCREMENT a táblához `materials`
--
ALTER TABLE `materials`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=97;

--
-- AUTO_INCREMENT a táblához `meals`
--
ALTER TABLE `meals`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=169;

--
-- AUTO_INCREMENT a táblához `mealtypes`
--
ALTER TABLE `mealtypes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- AUTO_INCREMENT a táblához `meassures`
--
ALTER TABLE `meassures`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT a táblához `roles`
--
ALTER TABLE `roles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `tokens`
--
ALTER TABLE `tokens`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=320;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=49;

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
  ADD CONSTRAINT `meals_ibfk_2` FOREIGN KEY (`MealTypeId`) REFERENCES `mealtypes` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE;

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
