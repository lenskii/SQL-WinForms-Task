# Приложение для учета счетов

Разработано приложение для учета счетов.

![1](https://github.com/lenskii/crmgurutesttask/blob/master/doc/1.jpg)

## База данных

Записи хранятся в БД на сервере MS SQL в таблице Invoice.

Бекап таблицы приведен в соответствующей [директории](https://github.com/lenskii/crmgurutesttask/tree/master/backup).

Имеющиеся поля:

* Уникальный идентификатор `Id` типа `int`
* Дата `date`типа `date`
* Клиент `client` типа `nvarchar(50)`
* Сумма счета `invoice_amount` типа `int`


## Пользовательский интерфейс

Написан на C# Windows Forms.

Среди имеющихся возможностей:

* Возможность добавлять запись в верхней части приложения (Дата выбирается с помощью элемента управления DateTimePicker).
* Общая сумма показываемых в данный момент счетов отображается в нижней части приложения
* Возможность изменять, удалять записи с помощью контекстного меню:

| Контекстное меню  | Диалоговое окно |
| ------------- | ------------- |
| ![2](https://github.com/lenskii/crmgurutesttask/blob/master/doc/2.jpg)  | ![3](https://github.com/lenskii/crmgurutesttask/blob/master/doc/3.jpg)  |

* Поиск по ФИО клиента:

![5](https://github.com/lenskii/crmgurutesttask/blob/master/doc/5.jpg)

* Обработаны исключения на нулевые значения и тип вводимых параметров:
![4](https://github.com/lenskii/crmgurutesttask/blob/master/doc/4.jpg)


Последний доступный релиз можно загрузить по соответствующей [ссылке](https://github.com/lenskii/crmgurutesttask/releases/tag/1.0).

---

Приветствуются любые комментарии и пожелания:

* lenskii97@gmail.com
* [Telegram](https://t.me/lenskii97)
