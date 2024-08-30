CREATE TABLE Books (
	bookId SERIAL,
	title varchar(255) NOT NULL,
	category varchar(255),
	publisher varchar(255) NOT NULL,
	isbn varchar(255) NOT NULL,
	description varchar(255),
	price int NOT NULL,
	stock int NOT NULL,
	deleteReason varchar(255),
	isDeleted bool NOT NULL,
	PRIMARY KEY (bookId)
)

CREATE TABLE Users (
	userId SERIAL,
	fName varchar(255) NOT NULL,
	lName varchar(255) NOT NULL,
	userPosition varchar(255) NOT NULL,
	userPrivilege varchar(255),
	PRIMARY KEY (userId)
)



	