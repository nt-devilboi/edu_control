CREATE DATABASE EduControl;

CREATE SCHEMA time_control

CREATE TABLE IF NOT EXISTS token
(
    value varchar(255) primary key not null,
    user_id uuid not null,
    create_dt date not null
)

CREATE TABLE IF NOT EXISTS account
(
    id uuid PRIMARY KEY not null,
    user_name varchar(256)  not null,
    email varchar(256) not null,
    password_hash text not null
)

CREATE TABLE IF NOT EXISTS book
(
    id          UUID primary key not null,
    user_id     UUID             not null,
    description varchar(40),
    name        varchar(20),
    constraint fk_user_id foreign key (user_id)
        references account (id)
)


CREATE TABLE IF NOT EXISTS status
(
    id          uuid PRIMARY KEY,
    user_id     uuid,
    name        varchar,
    description varchar,
    constraint fk_user_id foreign key (user_id)
        references account (id)

)


CREATE TABLE event
(
    id        UUID PRIMARY KEY not null,
    status_id UUID             NOT NULL,
    book_id   UUID             NOT NULL,
    start     timestamp        NOT NULL,
    "end"     timestamp,
    constraint fk_Status_id foreign key (status_id)
        references status (id),
    constraint fk_book_id foreign key (book_id)
        references book (id)
);



