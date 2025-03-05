CREATE DATABASE smartbuy_mobile CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
-- select * from information_schema.schemata WHERE schema_name='smartbuy_mobile';

USE smartbuy_mobile;

CREATE TABLE users(
    user_id 	 	CHAR(36) 						    PRIMARY KEY DEFAULT (UUID()), -- UUID
    name   		 	VARCHAR(100) 						NOT NULL, -- Full name
    gender 		 	ENUM('male', 'female', 'other') 	NOT NULL, -- Giới tính
    email 		 	VARCHAR(100)                        UNIQUE, -- email
    phone_number 	VARCHAR(10) 						UNIQUE, -- Số điện thoại
    password 	 	CHAR(60) 							NOT NULL, -- Bcrypt
    address 	 	VARCHAR(255), -- Địa chỉ
    role 		 	ENUM('user','admin') 				DEFAULT 'user', -- Quyền hạn
    avatar 			BLOB, -- Ảnh đại diện
    created_at 	 	TIMESTAMP			 				DEFAULT CURRENT_TIMESTAMP, -- Ngày tạo tài khoản
    
    CHECK (email IS NOT NULL OR phone_number IS NOT NULL) -- Phải có ít nhất một trong hai trường email hoặc phone_number
); 

CREATE TABLE vouchers (
    voucher_id              INT                         PRIMARY KEY AUTO_INCREMENT, -- ID voucher
    name                    VARCHAR(255)                NOT NULL, -- Tên voucher
    discount_percentage     DECIMAL(5, 2)               DEFAULT NULL, -- Phần trăm giảm giá
    discount_amount         DECIMAL(10, 2)              DEFAULT NULL, -- Số tiền giảm giá
    code                    VARCHAR(50)                 UNIQUE NOT NULL, -- Mã voucher
    min_order_value         DECIMAL(10, 2)              DEFAULT NULL, -- Giá trị đơn hàng tối thiểu
    max_discount_amount     DECIMAL(10, 2)              DEFAULT NULL, -- Số tiền giảm giá tối đa
    max_uses                INT                         DEFAULT NULL, -- Số lần sử dụng tối đa
    used_count              INT                         DEFAULT 0, -- Số lần đã sử dụng
    status                  ENUM('active', 'inactive')  NOT NULL, -- Trạng thái voucher
    start_date              DATE                        NOT NULL, -- Ngày bắt đầu áp dụng
    end_date                DATE                        NOT NULL -- Ngày kết thúc áp dụng
);

CREATE TABLE user_vouchers(
    user_id         CHAR(36), -- ID người dùng
    voucher_id      INT, -- ID voucher

    PRIMARY KEY (user_id, voucher_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (voucher_id) REFERENCES vouchers(voucher_id) ON DELETE CASCADE
);

CREATE TABLE brands(
	brand_id 	INT UNSIGNED 	AUTO_INCREMENT PRIMARY KEY, -- ID thương hiệu
    name 		VARCHAR(50) 	NOT NULL, -- Tên thương hiệu
    logo		BLOB -- Ảnh thương hiệu
);

CREATE TABLE colors(
    color_id 	INT UNSIGNED 	AUTO_INCREMENT PRIMARY KEY, -- ID màu sắc
    name 		VARCHAR(50) 	NOT NULL, -- Tên màu sắc
    code 		VARCHAR(7) -- Mã màu sắc (hex)
);

CREATE TABLE categories (
    category_id INT UNSIGNED	PRIMARY KEY AUTO_INCREMENT, -- ID danh mục
    name 		VARCHAR(255) 	NOT NULL, -- Tên danh mục
    brand_id 	INT UNSIGNED,
    
    FOREIGN KEY (brand_id) REFERENCES brands(brand_id) ON DELETE CASCADE
);

CREATE TABLE products(
	product_id 			INT UNSIGNED													AUTO_INCREMENT PRIMARY KEY, -- ID sản phẩm
    name 				VARCHAR(50) 													NOT NULL, -- Tên sản phẩm
    category_id 		INT UNSIGNED, -- ID danh mục
    color_id 			INT UNSIGNED, -- ID màu sắc
    stock_quantity 		INT UNSIGNED                                                    DEFAULT 0, -- Số lượng tồn kho
    import_price 		DECIMAL(12,2)													NOT NULL, -- Giá nhập
    selling_price 		DECIMAL(12,2)													NOT NULL, -- Giá bán
    status				ENUM('available','discontinued','in stock','out of stock') 		DEFAULT 'available', -- Trạng thái sản phẩm
    created_at 			TIMESTAMP                                                       DEFAULT CURRENT_TIMESTAMP, -- Ngày tạo sản phẩm
    description			TEXT, -- Mô tả sản phẩm
    rating				DECIMAL(2,1) CHECK (rating BETWEEN 0 AND 5) DEFAULT 0, -- Số sao trung bình
    rating_count		INT UNSIGNED                                                    DEFAULT 0, -- Số lượng đánh giá

    FOREIGN KEY (color_id) REFERENCES colors(color_id) ON DELETE SET NULL,
    FOREIGN KEY (category_id) REFERENCES categories(category_id) ON DELETE SET NULL
);

CREATE TABLE discounts (
    discount_id         INT UNSIGNED                    PRIMARY KEY AUTO_INCREMENT, -- ID giảm giá
    name                VARCHAR(255)                    NOT NULL, -- Tên giảm giá
    discount_percentage DECIMAL(5, 2)                   DEFAULT NULL, -- Giảm giá theo phần trăm
    discount_amount     DECIMAL(10, 2)                  DEFAULT NULL, -- Giảm giá theo số tiền
    status              ENUM('active', 'inactive')      NOT NULL, -- Trạng thái giảm giá
    start_date          DATE                            NOT NULL, -- Ngày bắt đầu
    end_date            DATE                            NOT NULL -- Ngày kết thúc
);

CREATE TABLE product_discounts (
    product_id 	INT UNSIGNED, -- ID sản phẩm
    discount_id INT UNSIGNED, -- ID voucher

    PRIMARY KEY (product_id, discount_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE CASCADE,
    FOREIGN KEY (discount_id) REFERENCES discounts(discount_id) ON DELETE CASCADE
);

CREATE TABLE product_details(
    product_detail_id 	INT UNSIGNED        AUTO_INCREMENT PRIMARY KEY, -- ID chi tiết sản phẩm
    product_id 			INT UNSIGNED, -- ID sản phẩm
    warranty 			VARCHAR(30), -- Thời gian bảo hành
    ram 				VARCHAR(10)         NOT NULL, -- Dung lượng RAM
    storage 			VARCHAR(10)         NOT NULL, -- Dung lượng bộ nhớ trong
    screen_size 		VARCHAR(10)         NOT NULL, -- Kích thước màn hình
    screen_resolution 	VARCHAR(20)         NOT NULL, -- Độ phân giải
    battery_capacity 	VARCHAR(20)         NOT NULL, -- Dung lượng pin
    processor 			VARCHAR(50)         NOT NULL, -- Tên vi xử lý
    charging_port 		VARCHAR(20), -- Cổng sạc
    sim_slot 			INT UNSIGNED, -- Số khe SIM
    os                  VARCHAR(20), -- Hệ điều hành

    FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE CASCADE
);

CREATE TABLE product_images(
    product_image_id 	INT UNSIGNED 	AUTO_INCREMENT PRIMARY KEY, -- ID ảnh sản phẩm
    product_id 		    INT UNSIGNED, -- ID sản phẩm
    image_path 		    VARCHAR(255) 	NOT NULL, -- Đường dẫn đến ảnh sản phẩm
    is_primary 		    BOOL            DEFAULT FALSE, -- Ảnh chính
    created_at 		    TIMESTAMP       DEFAULT CURRENT_TIMESTAMP, -- Ngày tạo ảnh sản phẩm

    FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE CASCADE
);

CREATE TABLE bills(
	bill_id 				INT UNSIGNED					AUTO_INCREMENT PRIMARY KEY, -- ID hóa đơn
    user_id 				CHAR(36), -- ID người dùng
    total_price 			DECIMAL(12,2)					NOT NULL, -- Tổng giá trị hóa đơn
	shipping_address 		VARCHAR(255) 					NOT NULL, -- Địa chỉ giao hàng
    shipping_fee 			DECIMAL(12,2)					NOT NULL, -- Phí vận chuyển
    status 					ENUM('unpaid','paid') 			DEFAULT 'unpaid', -- Trạng thái hóa đơn
    created_at 				TIMESTAMP 						DEFAULT CURRENT_TIMESTAMP, -- Ngày tạo hóa đơn
    
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE SET NULL
);

CREATE TABLE order_items(
	order_item_id 		INT UNSIGNED 					AUTO_INCREMENT PRIMARY KEY, -- ID sản phẩm trong hóa đơn
    bill_id 			INT UNSIGNED, -- ID hóa đơn
    product_id 			INT UNSIGNED, -- ID sản phẩm
    quantity 			INT UNSIGNED 					NOT NULL DEFAULT 1, -- Số lượng
    price 				DECIMAL(12,2)					NOT NULL, -- Giá sản phẩm
    
    FOREIGN KEY (bill_id) REFERENCES bills(bill_id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE SET NULL
);

CREATE TABLE carts (
	cart_id 		INT UNSIGNED 			AUTO_INCREMENT PRIMARY KEY, -- ID giỏ hàng
	user_id 		CHAR(36) 				NOT NULL, -- ID người dùng
	created_at 		TIMESTAMP 				DEFAULT CURRENT_TIMESTAMP, -- Ngày tạo giỏ hàng
    
	FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
);

CREATE TABLE cart_items (
    cart_item_id 	INT UNSIGNED 	    AUTO_INCREMENT PRIMARY KEY, -- ID sản phẩm trong giỏ hàng
    cart_id 		INT UNSIGNED, -- ID giỏ hàng
	product_id 		INT UNSIGNED, -- ID sản phẩm
	quantity 		INT UNSIGNED 		DEFAULT 1, -- Số lượng sản phẩm

	FOREIGN KEY (cart_id) REFERENCES carts(cart_id) ON DELETE CASCADE,
	FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE CASCADE
);

CREATE TABLE reviews(
	review_id 		INT UNSIGNED 			AUTO_INCREMENT PRIMARY KEY, -- ID đánh giá
    user_id 		CHAR(36), -- ID người dùng
    product_id 		INT UNSIGNED, -- ID sản phẩm
    rating 			INT UNSIGNED 			NOT NULL CHECK (rating BETWEEN 0 AND 5), -- Số sao
    comment 		TEXT, -- Bình luận
    created_at 		TIMESTAMP 				DEFAULT CURRENT_TIMESTAMP, -- Ngày tạo đánh giá
    
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE SET NULL,
    FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE CASCADE
);

CREATE TABLE shipments(
	shipment_id 	INT UNSIGNED 																	AUTO_INCREMENT PRIMARY KEY, -- ID vận chuyển
    bill_id 		INT UNSIGNED, -- ID hóa đơn
    status 			ENUM('pending','ready for pickup','shipping','on delivery','delivered') 		default 'pending', -- Trạng thái vận chuyển
    created_at 		TIMESTAMP 																		DEFAULT CURRENT_TIMESTAMP, -- Ngày tạo vận chuyển
    updated_at 		TIMESTAMP 																		DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Ngày cập nhật vận chuyển
    
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