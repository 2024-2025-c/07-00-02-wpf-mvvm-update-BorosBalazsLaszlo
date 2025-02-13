using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using Kreta.HttpService.Services;
using Kreta.Shared.Models.Entites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.SchoolSubjects
{
    public partial class SubjectsManagmentViewModel : BaseViewModel

    {
        private readonly ISubjectHttpService _httpService;

        //2. A lekért adatok ebben az adatstruktúrában jelennek majd meg a view-n
        [ObservableProperty]
        private ObservableCollection<Subject> _subjects = new ObservableCollection<Subject>();

        //2.b Az adatstruktúra a kiválasztott tantárgynak
        [ObservableProperty]
        private Subject _selectedSubject = new Subject();


        public SubjectsManagmentViewModel()
        {
        }

        //1.b Konstruktorban injelktáljuk a SubjectHttpService-t

        public SubjectsManagmentViewModel(ISubjectHttpService httpService)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
        }

        //1. feladat: Tantárgyak adatainak lekérése a backendről (vizsgaremek)
        //1.a: Adatok menüpont kiválasztására jelennek meg: InitializeAsync

        public override async Task InitializeAsync()
        {
            await UpdateViewAsync();
            await base.InitializeAsync();
        }

        [RelayCommand]
        private void DoNewSubject()
        {
            ClearForm();
        }

        [RelayCommand]
        private async Task DoDeleteSubject(Subject subject)
        {
            if (subject is not null)
            {
                await _httpService.DeleteAsync(subject.Id);
                ClearForm();
                await UpdateViewAsync();
            }
        }

        //1.c Adatok bekérése a backendről
        private async Task UpdateViewAsync()
        {
            //1.d HttpService-en keresztül backend hívás
            List<Subject> subjects = await _httpService.GetAllAsync();

            //2.a Lekért adatokat beletöltjük a Subjects-be
            Subjects = new ObservableCollection<Subject>(subjects);
        }

        // Felkészülés a dolgozatra

        private void ClearForm()
        {
            SelectedSubject = new Subject();
            OnPropertyChanged(nameof(SelectedSubject));
        }
    }
}
