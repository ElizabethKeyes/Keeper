CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

CREATE TABLE keeps(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT "Primary Key",
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(100) NOT NULL,
  description VARCHAR(500) NOT NULL,
  img VARCHAR(500) NOT NULL,
  views INT NOT NULL DEFAULT 0,

  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8mb4 COMMENT '';

CREATE TABLE vaults(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT "Primary Key",
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(100) NOT NULL,
  description VARCHAR(500) NOT NULL,
  img VARCHAR(500) NOT NULL,
  isPrivate BOOLEAN NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8mb4 COMMENT '';
