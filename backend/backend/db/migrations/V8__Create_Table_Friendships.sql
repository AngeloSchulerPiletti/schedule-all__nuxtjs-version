CREATE TABLE `friendships` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`sender_id` INT NOT NULL,
	`receiver_id` INT NOT NULL,
	`invite_accepted` TINYINT NOT NULL DEFAULT '0',
	PRIMARY KEY (`id`)
)
ENGINE=InnoDB DEFAULT CHARSET=latin1;