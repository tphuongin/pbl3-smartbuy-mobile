CREATE DATABASE smartbuy_mobile CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
-- select * from information_schema.schemata WHERE schema_name='smartbuy_mobile';

USE smartbuy_mobile;

CREATE TABLE users(
    user_id 	 	CHAR(36) 							PRIMARY KEY DEFAULT (UUID()),
    name   		 	VARCHAR(100) 						NOT NULL,
    gender 		 	ENUM('male', 'female', 'other') 	NOT NULL,
    email 		 	VARCHAR(50),
    phone_number 	VARCHAR(10) 						NOT NULL UNIQUE CHECK (phone_number REGEXP '^[0-9]{10}$'),
    password 	 	CHAR(60) 							NOT NULL, -- Bcrypt
    address 	 	VARCHAR(255),
    role 		 	ENUM('user','admin') 				DEFAULT 'user',
    avatar 			MEDIUMBLOB, 
    created_at 	 	TIMESTAMP			 				DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE brands(
	brand_id 	INT UNSIGNED 	AUTO_INCREMENT PRIMARY KEY,
    name 		VARCHAR(50) 	NOT NULL,
    logo		MEDIUMBLOB
);

CREATE TABLE products(
	product_id 			INT UNSIGNED													AUTO_INCREMENT PRIMARY KEY,
    name 				VARCHAR(50) 													NOT NULL,
    color 				VARCHAR(30) 													NOT NULL,
    brand_id 			INT UNSIGNED,
    stock_quantity 		INT UNSIGNED													DEFAULT 0,
    import_price 		DECIMAL(12,2)													NOT NULL,
    selling_price 		DECIMAL(12,2)													NOT NULL,
    status				ENUM('available','discontinued','in stock','out of stock') 		DEFAULT 'available',
    created_at 			TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    product_image		MEDIUMBLOB,
    detail				TEXT,
    
    FOREIGN KEY (brand_id) REFERENCES brands(brand_id) ON DELETE SET NULL
);

CREATE TABLE bills(
	bill_id 				INT UNSIGNED					AUTO_INCREMENT PRIMARY KEY,
    user_id 				CHAR(36),
    total_price 			DECIMAL(12,2)					NOT NULL,
	shipping_address 		VARCHAR(255) 					NOT NULL,
    status 					ENUM('unpaid','paid') 			DEFAULT 'unpaid',
    created_at 				TIMESTAMP 						DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE SET NULL
);

CREATE TABLE order_items(
	order_item_id 		INT UNSIGNED 					AUTO_INCREMENT PRIMARY KEY,
    bill_id 			INT UNSIGNED,
    product_id 			INT UNSIGNED,
    quantity 			INT UNSIGNED 					NOT NULL DEFAULT 1,
    price 				DECIMAL(12,2)					NOT NULL,
    
    FOREIGN KEY (bill_id) REFERENCES bills(bill_id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE SET NULL
);

CREATE TABLE carts (
	cart_id 		INT UNSIGNED 			AUTO_INCREMENT PRIMARY KEY,
	user_id 		CHAR(36) 				NOT NULL,
	created_at 		TIMESTAMP 				DEFAULT CURRENT_TIMESTAMP,
    
	FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
);

CREATE TABLE cart_items (
	user_id 		CHAR(36),
	product_id 		INT UNSIGNED,
	quantity 		INT UNSIGNED 		DEFAULT 1,

	PRIMARY KEY (user_id, product_id),
	FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
-- 	FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE SET NULL // t nghĩ là nếu product xóa thì cái item trong cart xóa lun
	FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE CASCADE
);

CREATE TABLE reviews(
	review_id 		INT UNSIGNED 			AUTO_INCREMENT PRIMARY KEY,
    user_id 		CHAR(36),
    product_id 		INT UNSIGNED,
    rating 			INT UNSIGNED 			NOT NULL CHECK (rating BETWEEN 0 AND 5),
    comment 		TEXT,
    created_at 		TIMESTAMP 				DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE SET NULL,
    FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE CASCADE
);

CREATE TABLE shipments(
	shipment_id 	INT UNSIGNED 																	AUTO_INCREMENT PRIMARY KEY,
    bill_id 		INT UNSIGNED,
    status 			ENUM('pending','ready for pickup','shipping','on delivery','delivered') 		default 'pending',
    created_at 		TIMESTAMP 																		DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (bill_id) REFERENCES bills(bill_id) ON DELETE CASCADE
);

INSERT INTO users(name, password, role, gender, phone_number) 
VALUES ('admin',
        '$2a$10$PRWw9JULaiIRWO5/nJFpZuHF00ZGmJgp82bfhtm08rGnoV6dyDvPu', -- 12345
        'admin',
        'other',
        '0123456789'
);


SELECT * FROM smartbuy_mobile.users;

-- DROP DATABASE smartbuy_mobile;