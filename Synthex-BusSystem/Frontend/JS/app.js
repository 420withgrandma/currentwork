$(document).ready(function () {
    // ------------------------------
    // NAVIGATION & AUTHENTICATION
    // ------------------------------

    // Get authentication buttons
    const $authButton = $('#authButton');
    const $profileButton = $('#profileButton');
    const $logoutButton = $('#logoutButton');

    // Check if user is logged in by looking for currentUser in localStorage
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));

    // Check if we're on the auth page - FIXED to be more reliable
    const path = window.location.pathname;
    const isAuthPage = path.endsWith('auth.html') || path.includes('/auth.html');

    console.log("Current path:", path);
    console.log("Is auth page:", isAuthPage);

    // Only run this on non-auth pages
    if (!isAuthPage) {
        console.log("Running non-auth page logic");
        if (currentUser) {
            // User is logged in - show profile and logout buttons, hide login button
            $authButton.addClass('hidden');
            $profileButton.removeClass('hidden');
            $logoutButton.removeClass('hidden');

            // Set up logout functionality
            $logoutButton.on('click', function (e) {
                e.preventDefault();

                // Use SweetAlert2 if available
                if (typeof Swal !== 'undefined') {
                    Swal.fire({
                        title: 'Are you sure you want to log out?',
                        icon: 'question',
                        showCancelButton: true,
                        confirmButtonColor: '#718588',
                        cancelButtonColor: '#9A8C98',
                        confirmButtonText: 'Yes, log me out'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            logout();
                        }
                    });
                } else {
                    // Fallback to standard confirm if SweetAlert2 is not available
                    if (confirm('Are you sure you want to log out?')) {
                        logout();
                    }
                }
            });
        } else {
            // User is not logged in - show login button, hide profile and logout buttons
            $authButton.removeClass('hidden');
            $profileButton.addClass('hidden');
            $logoutButton.addClass('hidden');
        }
    } else {
        // On auth page, always show the login button and hide others
        console.log("Running auth page specific logic");
        $authButton.removeClass('hidden');
        $profileButton.addClass('hidden');
        $logoutButton.addClass('hidden');
    }

    // Logout function
    function logout() {
        // Clear current user
        localStorage.removeItem('currentUser');

        // Redirect to home page
        window.location.href = 'index.html';
    }

    // ------------------------------
    // AUTH PAGE FUNCTIONALITY
    // ------------------------------

    // Only run this code on the auth page
    if (isAuthPage) {
        console.log("Setting up auth page forms");

        // Check if the login form exists
        if ($('#loginForm').length) {
            console.log("Login form found");
        } else {
            console.error("Login form not found! Make sure you have a form with id 'loginForm'");
        }

        // Check if register form exists
        if ($('#registerForm').length) {
            console.log("Register form found");
        } else {
            console.error("Register form not found! Make sure you have a form with id 'registerForm'");
        }

        // Log if toggle links exist
        if ($('.auth-toggle-link').length) {
            console.log("Toggle links found:", $('.auth-toggle-link').length);
        } else {
            console.error("Auth toggle links not found! Check for elements with class 'auth-toggle-link'");
        }

        // Setup login form functionality
        $('#loginForm').on('submit', function (e) {
            console.log("Login form submitted");
            e.preventDefault();
            const email = $('#loginEmail').val();
            const password = $('#loginPassword').val();
        
            if (email && password) {
                fetch('http://localhost:5176/api/user/login', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ email, password })
                })
                .then(res => {
                    if (!res.ok) throw new Error('Invalid login');
                    return res.json();
                })
                .then(user => {
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    window.location.href = 'index.html';
                })
                .catch(err => {
                    Swal.fire({
                        icon: 'error',
                        title: 'Login Failed',
                        text: err.message || 'Invalid email or password',
                        confirmButtonColor: '#718588'
                    });
                });
            }
             else {
                if (typeof Swal !== 'undefined') {
                    Swal.fire({
                        icon: 'error',
                        title: 'Login Failed',
                        text: 'Please enter both email and password',
                        confirmButtonColor: '#718588'
                    });
                } else {
                    alert('Please enter both email and password');
                }
            }
        });
        

        // Setup registration form functionality
        $('#registerForm').on('submit', function (e) {
            console.log("Register form submitted");
            e.preventDefault();
            const name = $('#registerName').val();
            const email = $('#registerEmail').val();
            const password = $('#registerPassword').val();
            const confirmPassword = $('#registerConfirmPassword').val();

            // Simple validation
            if (!name || !email || !password) {
                if (typeof Swal !== 'undefined') {
                    Swal.fire({
                        icon: 'error',
                        title: 'Registration Failed',
                        text: 'Please fill in all fields',
                        confirmButtonColor: '#718588'
                    });
                } else {
                    alert('Please fill in all fields');
                }
                return;
            }

            if (password !== confirmPassword) {
                if (typeof Swal !== 'undefined') {
                    Swal.fire({
                        icon: 'error',
                        title: 'Registration Failed',
                        text: 'Passwords do not match',
                        confirmButtonColor: '#718588'
                    });
                } else {
                    alert('Passwords do not match');
                }
                return;
            }

            // Create new user
            const newUser = {
                name: name,
                email: email,
                password: password, // In a real app, never store passwords in plain text
                tickets: []
            };

            // Store user in localStorage
            localStorage.setItem('currentUser', JSON.stringify(newUser));

            // Show success and redirect
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    icon: 'success',
                    title: 'Registration Successful',
                    text: 'You have successfully created an account!',
                    timer: 1500,
                    showConfirmButton: false
                }).then(() => {
                    window.location.href = 'index.html';
                });
            } else {
                alert('Registration successful!');
                window.location.href = 'index.html';
            }
        });

        // Toggle between login and register forms
        $('.auth-toggle-link').on('click', function (e) {
            console.log("Toggle link clicked");
            e.preventDefault();
            $('.auth-form').toggleClass('hidden');
        });
    }

    // ------------------------------
    // JOURNEY PLANNER FUNCTIONALITY
    // ------------------------------

    const timetableContainer = $('#timetableBody');

    function fetchTimetable() {
        fetch('http://localhost:5176/api/timetable')
            .then(response => response.json())
            .then(timetableData => {
                timetableContainer.empty();
                if (timetableData.length > 0) {
                    timetableData.forEach(item => {
                        timetableContainer.append(`
                            <tr>
                                <td>${item.departure}</td>
                                <td>${item.arrival}</td>
                                <td>${item.departureTime}</td>
                                <td>${item.arrivalTime}</td>
                                <td>${item.duration}</td>
                                <td>${item.seats}</td>
                                <td>${item.price}</td>
                            </tr>
                        `);
                    });
                } else {
                    timetableContainer.append('<tr><td colspan="7">No timetable data available</td></tr>');
                }
            })
            .catch(error => {
                console.error('Error fetching timetable:', error);
                timetableContainer.html('<tr><td colspan="7">Error loading timetable</td></tr>');
            });
    }

    function populateStationDropdowns() {
        const departureDropdown = $('#departureStation');
        const arrivalDropdown = $('#arrivalStation');

        fetch('http://localhost:5176/api/stations')
            .then(response => response.json())
            .then(stations => {
                stations.forEach(station => {
                    departureDropdown.append(new Option(station.name, station.id));
                    arrivalDropdown.append(new Option(station.name, station.id));
                });
            })
            .catch(error => {
                console.error('Error fetching stations:', error);
            });
    }

    // Initialize page content
    $(document).ready(function() {
        populateStationDropdowns();
        fetchTimetable();
    });

    // ------------------------------
    // USER PROFILE FUNCTIONALITY 
    // ------------------------------

    // Check if we're on the profile page
    if ($('#profileContainer').length > 0) {
        // If no user is logged in, redirect to auth page
        if (!currentUser) {
            window.location.href = 'auth.html';
            return;
        }

        // Initialize profile
        loadUserProfile();
        loadUserTickets();
        setupProfileEventListeners();

        // Function to load user profile information
        function loadUserProfile() {
            // Update avatar with first letter of name
            $('#profileAvatar').text(currentUser.name.charAt(0));

            // Set user info in profile
            $('#profileName').text(currentUser.name);
            $('#profileEmail').text(currentUser.email);

            $('#infoName').text(currentUser.name);
            $('#infoEmail').text(currentUser.email);
            $('#infoPhone').text(currentUser.phone || 'Not provided');
            $('#infoAddress').text(currentUser.address || 'Not provided');

            // Pre-fill edit profile form
            $('#editName').val(currentUser.name);
            $('#editEmail').val(currentUser.email);
            $('#editPhone').val(currentUser.phone || '');
            $('#editAddress').val(currentUser.address || '');

            // Load notification preferences if available
            if (currentUser.preferences) {
                $('#emailNotifications').prop('checked', currentUser.preferences.emailNotifications);
                $('#smsNotifications').prop('checked', currentUser.preferences.smsNotifications);
                $('#marketingEmails').prop('checked', currentUser.preferences.marketingEmails);
            }
        }

        // Function to load user tickets
        function loadUserTickets() {
            // Check if user has tickets in localStorage
            if (currentUser.tickets && currentUser.tickets.length > 0) {
                // Hide the no tickets message
                $('#noTicketsMessage').hide();
                $('#ticketList').show();

                // Clear the ticket list
                $('#ticketList').empty();

                // Sort tickets by date (newest first)
                const sortedTickets = [...currentUser.tickets].sort((a, b) => {
                    return new Date(b.fromDateID) - new Date(a.fromDateID);
                });

                // Check if any tickets are upcoming or past
                const today = new Date();
                today.setHours(0, 0, 0, 0);

                // Add each ticket to the list
                sortedTickets.forEach(ticket => {
                    const ticketDate = new Date(ticket.fromDateID);
                    const status = ticketDate >= today ? 'upcoming' : 'past';
                    const statusBadge = status === 'upcoming' ?
                        '<span class="badge bg-success">Upcoming</span>' :
                        '<span class="badge bg-secondary">Completed</span>';

                    // Format date for display
                    const formattedDate = new Date(ticket.fromDateID).toLocaleDateString('en-GB', {
                        day: 'numeric',
                        month: 'long',
                        year: 'numeric'
                    });

                    const ticketItem = `
                    <div class="ticket-item" data-ticket-status="${status}">
                        <div class="d-flex justify-content-between align-items-start">
                            <div class="ticket-id">Ticket #${ticket.ticketID}</div>
                            ${statusBadge}
                        </div>
                        <div class="ticket-details">
                            <p><strong>Journey:</strong> ${ticket.fromCity} to ${ticket.toCity}</p>
                            <p><strong>Date:</strong> ${formattedDate} | <strong>Price:</strong> £${ticket.priceID.toFixed(2)}</p>
                            <p><strong>Passenger:</strong> ${currentUser.name}</p>
                        </div>
                        <div class="ticket-action">
                            <button class="ticket-btn view-ticket" data-ticket-id="${ticket.ticketID}">
                                <i class="fas fa-eye me-1"></i> View Ticket
                            </button>
                            <button class="ticket-btn ms-2 download-ticket" data-ticket-id="${ticket.ticketID}">
                                <i class="fas fa-download me-1"></i> Download
                            </button>
                        </div>
                    </div>
                    `;

                    $('#ticketList').append(ticketItem);
                });
            } else {
                // Show no tickets message
                $('#noTicketsMessage').show();
                $('#ticketList').hide();
            }
        }

        // Setup profile event listeners
        function setupProfileEventListeners() {
            // Edit Profile button
            $('#editProfileBtn').on('click', function () {
                $('#editProfileModal').modal('show');
            });

            // Save Profile Changes button
            $('#saveProfileChanges').on('click', function () {
                // Get form values
                const name = $('#editName').val();
                const email = $('#editEmail').val();
                const phone = $('#editPhone').val();
                const address = $('#editAddress').val();

                // Basic validation
                if (!name || !email) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Validation Error',
                        text: 'Name and email are required.',
                        confirmButtonColor: '#718588'
                    });
                    return;
                }

                // Update current user data
                currentUser.name = name;
                currentUser.email = email;
                currentUser.phone = phone;
                currentUser.address = address;

                // Save to localStorage
                localStorage.setItem('currentUser', JSON.stringify(currentUser));

                // Update UI
                loadUserProfile();

                // Close modal
                $('#editProfileModal').modal('hide');

                // Show success message
                Swal.fire({
                    icon: 'success',
                    title: 'Profile Updated',
                    text: 'Your profile has been updated successfully!',
                    timer: 1500,
                    showConfirmButton: false
                });
            });

            // Ticket filter dropdown
            $('.dropdown-item[data-filter]').on('click', function (e) {
                e.preventDefault();
                const filter = $(this).data('filter');

                if (filter === 'all') {
                    $('.ticket-item').show();
                } else {
                    $('.ticket-item').hide();
                    $(`.ticket-item[data-ticket-status="${filter}"]`).show();
                }
            });

            // View Ticket button
            $(document).on('click', '.view-ticket', function () {
                const ticketId = $(this).data('ticket-id');

                // Find the ticket
                const ticket = currentUser.tickets ? currentUser.tickets.find(t => t.ticketID == ticketId) : null;

                if (ticket) {
                    // Format date for display
                    const formattedFromDate = new Date(ticket.fromDateID).toLocaleDateString('en-GB', {
                        day: 'numeric',
                        month: 'long',
                        year: 'numeric'
                    });

                    const formattedToDate = new Date(ticket.toDateID).toLocaleDateString('en-GB', {
                        day: 'numeric',
                        month: 'long',
                        year: 'numeric'
                    });

                    // Generate QR code data
                    const qrData = `GOTIMELY-${ticket.ticketID}-${currentUser.email}`;

                    // Generate ticket detail HTML
                    const ticketHtml = `
                    <div style="text-align: center; margin-bottom: 20px;">
                        <i class="fa-solid fa-bus" style="font-size: 40px; color: #718588;"></i>
                        <h2 style="color: #4a5759; margin-top: 10px;">GoTimely</h2>
                    </div>
                    <div class="ticket-details-container">
                        <table class="table">
                            <tr>
                                <th>Ticket ID:</th>
                                <td>${ticket.ticketID}</td>
                            </tr>
                            <tr>
                                <th>From:</th>
                                <td>${ticket.fromCity}</td>
                            </tr>
                            <tr>
                                <th>To:</th>
                                <td>${ticket.toCity}</td>
                            </tr>
                            <tr>
                                <th>Departure Date:</th>
                                <td>${formattedFromDate}</td>
                            </tr>
                            <tr>
                                <th>Return Date:</th>
                                <td>${formattedToDate}</td>
                            </tr>
                            <tr>
                                <th>Passenger:</th>
                                <td>${currentUser.name}</td>
                            </tr>
                            <tr>
                                <th>Price:</th>
                                <td>£${ticket.priceID.toFixed(2)}</td>
                            </tr>
                        </table>
                        <div style="text-align: center; margin-top: 20px;">
                            <img src="https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=${encodeURIComponent(qrData)}" alt="QR Code">
                            <p style="margin-top: 10px; font-size: 12px; color: #777;">Scan this QR code to validate your ticket</p>
                        </div>
                    </div>
                    `;

                    // Update modal content and show
                    $('#ticketDetailContent').html(ticketHtml);
                    $('#ticketDetailModal').modal('show');
                } else {
                    // Ticket not found - handle error
                    Swal.fire({
                        icon: 'error',
                        title: 'Ticket Not Found',
                        text: 'The requested ticket could not be found.',
                        confirmButtonColor: '#718588'
                    });
                }
            });

            // Download Ticket button
            $(document).on('click', '.download-ticket, #downloadTicketPdf', function () {
                // In a real app, this would generate and download a PDF
                Swal.fire({
                    icon: 'success',
                    title: 'Ticket Downloaded',
                    text: 'Your ticket has been downloaded successfully.',
                    timer: 1500,
                    showConfirmButton: false
                });
            });

            // Account Settings form submission
            $('#accountSettingsForm').on('submit', function (e) {
                e.preventDefault();

                const currentPassword = $('#currentPassword').val();
                const newPassword = $('#newPassword').val();
                const confirmPassword = $('#confirmPassword').val();

                // Simple validation
                if (newPassword !== confirmPassword) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Password Mismatch',
                        text: 'New passwords do not match. Please try again.',
                        confirmButtonColor: '#718588'
                    });
                    return;
                }

                // Update the password (in a real app, this would be done securely)
                currentUser.password = newPassword;
                localStorage.setItem('currentUser', JSON.stringify(currentUser));

                // Reset form
                $('#accountSettingsForm')[0].reset();

                // Show success message
                Swal.fire({
                    icon: 'success',
                    title: 'Password Updated',
                    text: 'Your password has been updated successfully!',
                    timer: 1500,
                    showConfirmButton: false
                });
            });

            // Save Notification Preferences
            $('#saveNotificationPreferences').on('click', function () {
                // Get preference values
                const emailNotifications = $('#emailNotifications').is(':checked');
                const smsNotifications = $('#smsNotifications').is(':checked');
                const marketingEmails = $('#marketingEmails').is(':checked');

                // Update user preferences
                currentUser.preferences = {
                    emailNotifications,
                    smsNotifications,
                    marketingEmails
                };

                // Save to localStorage
                localStorage.setItem('currentUser', JSON.stringify(currentUser));

                // Show success message
                Swal.fire({
                    icon: 'success',
                    title: 'Preferences Saved',
                    text: 'Your notification preferences have been updated!',
                    timer: 1500,
                    showConfirmButton: false
                });
            });
        }
    }
});