CREATE TABLE `users` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`user_name` VARCHAR(50) NOT NULL,
	`password` VARCHAR(130) NOT NULL,
	`email` VARCHAR(100) NOT NULL,
	`full_name` VARCHAR(120) NOT NULL,
	`refresh_token` VARCHAR(500) NULL DEFAULT '0',
	`refresh_token_expiry_time` DATETIME NULL DEFAULT NULL,
	`wallet_address` VARCHAR(42) NOT NULL,
	PRIMARY KEY (`id`),
	UNIQUE `user_name` (`user_name`),
	UNIQUE `email` (`email`),
	UNIQUE `wallet_address` (`wallet_address`)
)
ENGINE=InnoDB DEFAULT CHARSET=latin1;