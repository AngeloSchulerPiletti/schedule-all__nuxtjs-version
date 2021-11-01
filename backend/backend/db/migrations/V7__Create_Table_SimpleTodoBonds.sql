CREATE TABLE `simpletodobonds` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`simpletodo_id` INT NOT NULL,
	`user_id` INT NOT NULL,
	`invite_accepted` TINYINT NOT NULL DEFAULT '0',
	PRIMARY KEY (`id`)
)
ENGINE=InnoDB DEFAULT CHARSET=latin1;