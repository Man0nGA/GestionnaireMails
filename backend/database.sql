CREATE TABLE "user"(
    id integer GENERATED ALWAYS AS IDENTITY NOT NULL,
    pseudo varchar(255) NOT NULL,
    email_address varchar(255) NOT NULL,
    PRIMARY KEY(id)
);

CREATE TABLE mail(
    id integer GENERATED ALWAYS AS IDENTITY NOT NULL,
    user_id integer NOT NULL,
    object varchar(255),
    body varchar(255),
    sender varchar(255) NOT NULL,
    receiver varchar(255) NOT NULL,
    inbox varchar(255) NOT NULL,
    PRIMARY KEY(id),
    CONSTRAINT mail_user_id_fkey FOREIGN key(user_id) REFERENCES "user"(id)
);