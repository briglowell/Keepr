-- CREATE TABLE keeps (
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     description VARCHAR(1000) NOT NULL,
--     img VARCHAR(255),
--     views INT NOT NULL,
--     shares INT NOT NULL,
--     PRIMARY KEY (id),
--     FOREIGN KEY (creatorId) REFERENCES profiles (id) ON DELETE CASCADE
-- )

-- CREATE TABLE vaults(
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     description VARCHAR(255),
--     isPrivate TINYINT NOT NULL,
--     PRIMARY KEY (id),
--     FOREIGN KEY (creatorId) REFERENCES profiles (id) ON DELETE CASCADE
-- )

-- drop table profile;

-- CREATE TABLE vaultkeeps (
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255) NOT NULL,
--     vaultId INT NOT NULL,
--     keepId INT NOT NULL,
--     PRIMARY KEY (id),
--     FOREIGN KEY (creatorId) REFERENCES profiles (id) ON DELETE CASCADE,
--     FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE,
--     FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE
-- )

-- CREATE TABLE profiles (
--     id VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     picture VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id)
-- )

