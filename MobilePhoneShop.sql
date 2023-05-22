drop database MobilePhoneShop;
create database MobilePhoneShop;

use MobilePhoneShop;

create table Account(
	account_id int auto_increment primary key,
    name varchar(100) not null,
    userName varchar(100) not null,
    password varchar(100) not null,
    phoneN varchar(10) not null,
    role int(10) not null
);
create table MobilePhone(
	mobilePhone_id int auto_increment primary key,
    mobilePhone_name varchar(50) not null,
    chip varchar(30) not null,
    memory varchar(50) not null,
    camera varchar(50) not null,
    operatingSystem varchar(20) not null,
    weight varchar(20) not null,
    pin varchar(10) not null,
    warrantyPeriod varchar(10) not null,
    price decimal(20,2) not null,
    amount int(10) not null
);
create table Customer(
    customer_id int auto_increment primary key,
    customer_name varchar(50) not null,
    customer_phonenumber varchar(12)not null UNIQUE,
    customer_address varchar(50)
);
create table Orders(
	order_id int auto_increment primary key,
    customer_id int not null,
    seller_id int not null,
    order_date datetime default now() not null,
    status int(10) not null,
    constraint fk_Orders_Customer FOREIGN KEY (customer_id) REFERENCES customer (customer_id),
    constraint fk_Orders_Account FOREIGN KEY (seller_id) REFERENCES account (account_id)
);
CREATE TABLE orderdetails (
  order_id int NOT NULL,
  mobilePhone_id int NOT NULL,
  unit_price decimal(20,2) NOT NULL,
  quantity int NOT NULL,
  PRIMARY KEY (order_id,mobilePhone_id),
  KEY fk_OrderDetails_Orders_mobileid_idx (mobilePhone_id),
  CONSTRAINT fk_OrderDetails_Orders_mobileid FOREIGN KEY (mobilePhone_id) REFERENCES mobilephone (mobilePhone_id) ON UPDATE CASCADE,
  CONSTRAINT fk_OrderDetails_Orders_orderid FOREIGN KEY (order_id) REFERENCES orders (order_id) ON UPDATE CASCADE
) ;
ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci


delimiter $$
create trigger tg_CheckAmount
	before update on mobilephone
	for each row
	begin
		if new.amount < 0 then
            signal sqlstate '45001' set message_text = 'tg_CheckAmount: amount must > 0';
        end if;
    end $$
delimiter ;

delimiter $$
create trigger tg_before_insert before insert
	on mobilephone for each row
    begin
		if new.amount < 0 then
            signal sqlstate '45001' set message_text = 'tg_before_insert: amount must > 0';
        end if;
    end $$
delimiter ;

-- DELIMITER $$
-- CREATE TRIGGER tg_orderdetail 
-- AFTER INSERT 
-- ON orderdetails for each row
-- BEGIN

-- 	UPDATE mobilephone
--     SET Amount = Amount - New.quantity
--     Where mobilephone_id = New.mobilephone_id;
-- END;
-- $$
--  DELIMITER $$ 


--  DROP TRIGGER IF EXISTS tg_insertorderdetails; 
--  DELIMITER $$ 
-- CREATE TRIGGER tg_insertorderdetails 
--  AFTER INSERT  
--  ON orderdetails for each row 
--  BEGIN 
  
-- 		UPDATE mobilephone 
--      SET Amount = Amount - New.Quantity 
--      Where mobilePhone_id = New.mobilePhone_id; 
--  END;  
-- DELIMITER $$

 DELIMITER $$
CREATE TRIGGER tg_order_status 
AFTER UPDATE
ON orders for each row
BEGIN
	if new.status = 0 then 
		delete from orderdetails
		Where order_id = New.order_id;
	end if;
END;
$$
DELIMITER $$

-- DELIMITER $$ 
-- CREATE TRIGGER tg_orderdetails_mobilephone 
-- AFTER UPDATE
-- ON orderdetails for each row
-- BEGIN
-- 		UPDATE mobilephone
-- 		SET Amount = Amount + orderdetails.quantity
-- 		Where mobilephone_id = New.mobilephone_id;
-- END;
-- $$
-- DELIMITER $$ 

INSERT INTO `mobilephoneshop`.`account` (`account_id`, `name`, `userName`, `password`, `phoneN`, `role`) VALUES ('1', 'Bui Quoc Cuong', 'admin', 'thanh', '0999999999', '1');
INSERT INTO `mobilephoneshop`.`account` (`account_id`, `name`, `userName`, `password`, `phoneN`, `role`) VALUES ('2', 'Nguyen Tien Thanh', 'seller', 'cuong', '0111111111', '2');
INSERT INTO `mobilephoneshop`.`account` (`account_id`, `name`, `userName`, `password`, `phoneN`, `role`) VALUES ('3', 'Nguyen Tien Cuong', 'thanhdz', 'cuongdz', '0222222222', '2');

INSERT INTO `mobilephoneshop`.`customer` (`customer_id`, `customer_name`, `customer_phonenumber`, `customer_address`) VALUES ('1', 'Nguyen Tien HAHA', '0123123123', 'Ha Noi');

INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('1', 'OPPO A94', 'Helio P95', '128 GB', '48.0 MP ', 'Android', '0.2kg', '4310 mAh', '3 year', '5490000', '13');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('2', 'OPPO A95', 'Snapdragon 662', '128 GB', '48.0 MP', 'Android', '0.2kg', '5000 mAh', '3 year', '5990000', '16');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('3', 'OPPO Reno6 Z 5G', 'MediaTek Dimensity 800U', '128 GB', '64.0 MP', 'Android', '0.3kg', '4310 mAh', '3 year', '6990000', '10');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('4', 'Samsung Galaxy A53', 'Exynos 1280', '256 GB', '64.0 MP', 'Android', '0.3kg', '5000 mAh', '2 year', '10990000', '20');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('5', 'iPhone 11', 'Apple A13 Bionic', '64 GB', '12.0 MP', 'IOS', '0.2kg', '3110 mAh', '1 year', '11499000', '5');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('6', 'Samsung Galaxy S21', 'Exynos 2100', '128 GB', '12.0 MP', 'Android', '0.4kg', '4500 mAh', '2 year', '12990000', '11');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('7', 'iPhone 13 Pro Max', 'Apple A15 Bionic', '128 GB', '12.0 MP', 'IOS', '0.3kg', '4352 mAh', '3 year', '27490000', '30');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('8', 'Samsung Galaxy Z Fold3 5G', 'Snapdragon 888', '256 GB', '12.0 MP', 'Android', '0.2kg', '4400 mAh', '2 year', '29990000', '11');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('9', 'Samsung Galaxy A33 5G', 'Exynos 1280', '128 GB', '48.0 MP', 'Android', '0.4kg', '5000 mAh', '1 year', '7290000', '3');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('10', 'iPhone SE 2022', 'Apple A15 Bionic', '64 GB', '12.0 MP', 'IOS', '0.2kg', '15 h', '1 year', '11290000', '4');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('11', 'Samsung Galaxy Z Flip4', 'Snapdragon 8+ Gen 1', '128 GB', '12.0 MP', 'Android', '0.4kg', '3700 mAh', '3 year', '20990000', '15');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('12', 'Samsung Galaxy Z Flip3', 'Snapdragon 888', '256 GB', '12.0 MP', 'Android', '0.4kg', '3300 mAh', '2 year', '18490000', '10');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('13', 'Iphone 14 pro max','Apple A15 Bionic', '128GB', '12.0 MP', 'IOS', '0.2kg', '4352 mAh', '4year', '24990000', '30');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('14', 'Samsung Galaxy S22', 'Snapdragon 8 Gen 1', '128 GB', '108.0 MP', 'Android', '0.3kg', '5000 mAh', '3 year', '25990000', '21');
INSERT INTO `mobilephoneshop`.`mobilephone` (`mobilePhone_id`, `mobilePhone_name`, `chip`, `memory`, `camera`, `operatingSystem`, `weight`, `pin`, `warrantyPeriod`, `price`, `amount`) VALUES ('15', 'OPPO Find X5 Pro', 'Snapdragon 8 Gen 1', '256 GB', '50.0 MP', 'Android', '0.2kg', '5000 mAh', '2 year', '27990000', '10');
