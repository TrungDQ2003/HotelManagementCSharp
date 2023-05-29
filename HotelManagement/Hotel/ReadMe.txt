Table userHT không sử dụng trong project này nên có thể xóa đi
Username và password mặc định sau khi được mã hóa là admin

Query SQL:

CREATE DATABASE phongdb
GO
USE phongdb
GO
CREATE TABLE booking(
	id_booking INT  NOT NULL,
	id_room INT  NOT NULL,
	username char (20) NOT NULL,
	time INT  NOT NULL,
	date_booked char(20) NOT NULL,
	status INT  NOT NULL,
	CONSTRAINT pk_booking PRIMARY KEY(id_booking)
)

CREATE TABLE room(
	id_room INT  NOT NULL,
	name char (20) NOT NULL,
	price DECIMAL  NOT NULL,
	status INT NOT NULL,
	image char(20) NOT NULL,
)

CREATE TABLE userHT(
	id INT  NOT NULL,
	username char (20) NOT NULL,
	password char (20) NOT NULL,
	name char (20) NOT NULL,
	balance char(20) NOT NULL,
	birthday char(20) NOT NULL,
	phone char(20) NOT NULL,
	status INT NOT NULL,
)

INSERT INTO room(id_room,name,price,status,image) VALUES(1,'Phong So 1', 100000,1,'')
INSERT INTO room(id_room,name,price,status,image) VALUES(2,'Phong So 2', 100000,1,'')
INSERT INTO room(id_room,name,price,status,image) VALUES(3,'Phong So 3', 100000,1,'')


INSERT INTO userHT(id,username,password,name,balance,birthday,phone,status) VALUES(1,'user1', '123456','Nguoi dung 1','200','','09642564356',1)
INSERT INTO userHT(id,username,password,name,balance,birthday,phone,status) VALUES(2,'user2', '123456','Nguoi dung 2','400','','04831962685',1)
