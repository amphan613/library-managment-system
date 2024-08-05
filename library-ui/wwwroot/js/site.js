

function showPage(pageName) {

	// Determine the URL based on the pageName
	var url = "";
	switch (pageName) {
		case "Default":
			url = "/Home/Default"; // Adjust the URL as needed
			break;
		case "AddBook":
			url = "/Home/AddBook"; // Adjust the URL as needed
			break;

		// Add other cases for different pages
	}

	// Use jQuery to load the content
	if (url) {
		$("#mainContent").load(url);
	}

	if (pageName != "Default")
		highlightMenuItem(pageName);
}

function toggleMenu() {
	var menu = document.getElementById('menu');
	var toggleButton = document.getElementById('toggleButton');
	if (menu.style.transform == 'translateX(0px)') {
		menu.style.transform = 'translateX(-250px)';
		toggleButton.innerHTML = '+';
	} else {
		menu.style.transform = 'translateX(0px)';
		toggleButton.innerHTML = '-';
	}
}

function highlightMenuItem(pageName) {
	// Remove active class from all menu items
	$('li[data-page]').removeClass('active-menu-item');

	// Add active class to the current page's menu item
	$(`li[data-page="${pageName}"]`).addClass('active-menu-item');
}