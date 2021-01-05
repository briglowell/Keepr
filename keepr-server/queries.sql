-- CREATE TABLE keeps (
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255),
--     name VARCHAR(80),
--     description VARCHAR(1000),
--     img VARCHAR(255),
--     views INT NOT NULL,
--     shares INT NOT NULL,
--     PRIMARY KEY (id),
--     FOREIGN KEY (creatorId) references profile(id) on delete cascade
-- )

-- CREATE TABLE vaults(
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255),
--     name VARCHAR(80),
--     description VARCHAR(255),
--     isPrivate TINYINT,
--     PRIMARY KEY (id)
-- )

-- CREATE TABLE vaultkeeps (
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255),
--     vaultId VARCHAR(255),
--     keepId VARCHAR(255),
--     PRIMARY KEY (id)
-- )

-- CREATE TABLE profile (
--     id VARCHAR(255),
--     name VARCHAR(80),
--     email VARCHAR(255),
--     picture VARCHAR(255),
--     PRIMARY KEY (id)
-- )

