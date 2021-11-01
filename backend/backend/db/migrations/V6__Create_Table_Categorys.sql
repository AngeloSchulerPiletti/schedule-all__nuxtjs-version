CREATE TABLE `categorys` (
	`category_id` INT(11) NOT NULL AUTO_INCREMENT,
	`user_id` INT NOT NULL,
	`title` VARCHAR(200) NOT NULL,
	PRIMARY KEY (`category_id`)
)
ENGINE=InnoDB DEFAULT CHARSET=latin1;