-- 게시판 테이블
CREATE TABLE [dbo].[Articles]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1), -- 일련번호
	[Title] nvarchar(255) not null, -- 게시판의 제목

	-- TODO: Columns Add Region
	[Content] nvarchar(MAX) Null, -- 내용

	[IsPinned] Bit null default(0), -- 공지글로 올리기

	-- AuditableBase.cs 참조
	[CreatedBy] nvarchar(255) null, -- 등록자
	[Created] DateTime default(getDate()), -- 생성일
	[ModifiedBy] nvarchar(255) null, -- 수정자
	[Modified] datetime null -- 수정일
)
GO
