CREATE SCHEMA edu_control

CREATE TABLE IF NOT EXISTS token
(
    value text primary key not null,
    user_id varchar(256) not null,
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

CREATE TABLE event (
  id UUID PRIMARY KEY not null ,
  status_id UUID NOT NULL,
  book_id UUID NOT NULL,
  start TIMESTAMPTZ NOT NULL
)

CREATE TABLE status (
    id uuid PRIMARY KEY,
    user_id uuid,
    name varchar,
    description varchar
);

INSERT INTO edu_control.account
    VAlUES ('f66dbda6-10ef-4aea-ae13-6b83fbcef433', 'devil', 'turnickii.n@gmail.com', 'asldfjasdfawvasdfja');

INSERT INTO edu_control.book VALUES('5998216c-00d9-4ead-b598-941b49a8c580', 'f66dbda6-10ef-4aea-ae13-6b83fbcef433', 'sdfads', 'react')