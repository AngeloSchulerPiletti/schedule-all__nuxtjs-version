CREATE TABLE `simpletodos` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`created_at` DATETIME NOT NULL,
	`finished_at` DATETIME NULL DEFAULT NULL,
	`finished` TINYINT NOT NULL DEFAULT '0',
	`title` VARCHAR(100) NOT NULL DEFAULT '0',
	`description` VARCHAR(500) NULL DEFAULT NULL,
	`category_id` INT NULL DEFAULT NULL,
	`user_id` INT NOT NULL,
	PRIMARY KEY (`id`)
)
ENGINE=InnoDB DEFAULT CHARSET=latin1;