-- phpMyAdmin SQL Dump
-- version 4.7.7
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Июл 11 2018 г., 23:18
-- Версия сервера: 5.6.38
-- Версия PHP: 5.5.38

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `NotebookDB`
--
CREATE DATABASE IF NOT EXISTS `NotebookDB` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `NotebookDB`;

-- --------------------------------------------------------

--
-- Структура таблицы `Users`
--

CREATE TABLE `Users` (
  `Id` int(11) NOT NULL,
  `Address` text,
  `Birthday` datetime NOT NULL,
  `Name` text,
  `Surname` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `Users`
--

INSERT INTO `Users` (`Id`, `Address`, `Birthday`, `Name`, `Surname`) VALUES
(1, 'St. Gorman 1', '1996-09-25 00:00:00', 'John', 'Smith'),
(2, 'St. Germany 14', '1989-11-06 00:00:00', 'Sarah', 'Smith'),
(3, 'Center Park', '1999-03-15 00:00:00', 'Bob', 'Parker'),
(4, 'Prokupe st.', '1985-06-30 00:00:00', 'Kim', 'Gray'),
(5, 'Baimble 12', '1979-02-19 00:00:00', 'Paul', 'Larcraft'),
(6, 'Library', '2000-05-10 00:00:00', 'Remy', 'Blackwood'),
(7, 'Eston st.', '1999-04-11 00:00:00', 'Bob', 'Redkliff');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Users`
--
ALTER TABLE `Users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
