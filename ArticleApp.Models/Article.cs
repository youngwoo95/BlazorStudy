using Dul.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ArticleApp.Models
{
    /// <summary>
    /// 게시판(Article) 모델 클래스 : Articles 테이블에 1:1로 매핑
    /// </summary>
    public class Article : AuditableBase
    {

        /// <summary>
        /// 일련번호
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 게시판의 제목
        /// </summary>
        [Required(ErrorMessage = "필수입력 입니다.")]
        public string Title { get; set; }


    }
}
