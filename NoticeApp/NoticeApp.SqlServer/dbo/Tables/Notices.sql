-- [1] Table: Notice(공지사항) 테이블
CREATE TABLE [dbo].[Notices]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1, 1), -- Serial Number
	[ParentId] Int Null, -- ParentId, AppId, SiteID
	
	[Name] nvarchar(100) Not null, -- 작성자
	[Title] nvarchar(255) null, -- 제목
	[Content] nvarchar(MAX) null, -- 내용

	[IsPinned] Bit null default(0), -- 공지글로 올리기
	
	[CreatedBy] nvarchar(255) null, -- 등록자(Creator)
	[Created] datetime default(GetDate()) null, -- 생성일(PostDate)
	[ModifiedBy] nvarchar(255) null, -- 수정자(LastModifiedBy)
	[Modified] Datetime null, -- 수정일 (LastModified)
)
GO