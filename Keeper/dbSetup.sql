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

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';

CREATE TABLE vaults(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT "Primary Key",
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(100) NOT NULL,
  description VARCHAR(500) NOT NULL,
  img VARCHAR(500) NOT NULL,
  isPrivate BOOLEAN NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';

CREATE TABLE vaultKeeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT "Primary Key",
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';

  SELECT
    keeps.*,
    COUNT(vaultKeeps.id) AS kept,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    GROUP BY (keeps.id);

    INSERT INTO keeps
    (creatorId, name, description, img)
    VALUES
    ("641dcead96ec6728cb1f8b95", "Testing Keep", "This is a test keep.", "https://images.unsplash.com/photo-1583394885876-f7744b77051f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=436&q=80");

    SELECT
    keeps.*,
    COUNT(vaultKeeps.id) AS kept,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    WHERE keeps.id = LAST_INSERT_ID()
    GROUP BY (keeps.id);