using Microsoft.AspNetCore.Components;
using NoticeApp.Models;
using NoticeApp.Pages.Notices.Component;

namespace NoticeApp.Pages.Notices
{
    public partial class Manage
    {
        [Parameter]
        public int ParentId { get; set; }

        [Inject]
        public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

        [Inject]
        public NavigationManager NavigationManagerReference { get; set; }

        public EditorForm EditorFormReference { get; set; }
        
        public DeleteDialog DeleteDialogReference { get; set; }

        protected List<Notice> models;

        protected Notice model = new();

        /// <summary>
        /// 공지사항으로 올리기 폼을 띄울건지 여부
        /// </summary>
        public bool IsInlineDialogShow { get; set; } = false;

        protected DulPager.DulPagerBase pager = new()
        {
            PageNumber = 1,
            PageIndex = 0,
            PageSize = 2,
            PagerButtonCount = 5
        };


        protected override async Task OnInitializedAsync()
        {
            await DisplayData();
        }

        private async Task DisplayData()
        {
            if(ParentId == 0)
            {
                //await Task.Delay(3000);

                var resultSet = await NoticeRepositoryAsyncReference.GetAllAsync(pager.PageIndex, pager.PageSize);
                pager.RecordCount = resultSet.TotalRecords;
                models = resultSet.Records.ToList();
            }
            else
            {
                var resultSet = await NoticeRepositoryAsyncReference.GetAllByParentIdAsync(pager.PageIndex, pager.PageSize, ParentId);
                pager.RecordCount = resultSet.TotalRecords;
                models = resultSet.Records.ToList();
            }


            StateHasChanged();
        }

        protected void NameClick(int id)
        {
            NavigationManagerReference.NavigateTo($"/Notices/Details/{id}");
        }

        protected async void PageIndexChanged(int pageIndex)
        {
            pager.PageIndex = pageIndex;
            pager.PageNumber = pageIndex + 1;

            await DisplayData(); // 다시호출

        }

        public string EditorFormTitle { get; set; } = "CREATE";

        protected void ShowEditorForm()
        {
            EditorFormTitle = "CREATE";

            this.model = new();
            EditorFormReference.Show();
        }

        protected void EditBy(Notice model)
        {
            EditorFormTitle = "EDIT";

            this.model = model;
            EditorFormReference.Show();
        }

        protected void DeleteBy(Notice model)
        {
            this.model = model;

            DeleteDialogReference.Show();
        }

        protected void ToggleBy(Notice model)
        {
            this.model = model;

            IsInlineDialogShow = true;
        }

        protected async void DeleteClick()
        {
            await NoticeRepositoryAsyncReference.DeleteAsync(this.model.Id);
            
            DeleteDialogReference.Hide(); // 폼닫기
            
            this.model = new(); // 해당 모델 비우기

            await DisplayData();
        }


        protected async void CreateOrEdit()
        {
            EditorFormReference.Hide(); // 숨겨줌
            await DisplayData(); // 다시호출

        }

        protected async void ToggleClick()
        {
            this.model.IsPinned = (this.model?.IsPinned == true) ? false : true; // 토글기능
            await NoticeRepositoryAsyncReference.EditAsync(this.model);

            IsInlineDialogShow = false; // 다이얼로그 닫기
            this.model = new(); // 해당 모델 비우기
            await DisplayData();
        }

        protected void ToggleClose()
        {
            IsInlineDialogShow = false;
            this.model = new(); // 선택한 모델 초기화
        }

 

    }
}
