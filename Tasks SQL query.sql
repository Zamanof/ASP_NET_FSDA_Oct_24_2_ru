-- =========================================
-- Seed 50 TaskItems with valid ProjectId
-- =========================================

DECLARE @Tasks TABLE
(
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    Status INT NOT NULL,
    Priority INT NOT NULL,
    ProjectId INT NOT NULL
);

INSERT INTO @Tasks (Title, Description, Status, Priority, ProjectId)
VALUES
-- Project 5
('Create project structure', 'Initialize solution structure and base folders', 2, 2, 5),
('Configure database connection', 'Setup SQL Server connection string and migrations', 2, 2, 5),
('Add Task entity', 'Create Task entity with relationships and constraints', 2, 1, 5),
('Implement Project entity', 'Add Project model and configure EF Core mapping', 2, 1, 5),
('Setup AutoMapper profiles', 'Configure DTO mappings for Task and Project', 1, 1, 5),
('Add CRUD for projects', 'Implement create, update, delete and read operations for projects', 1, 2, 5),
('Add CRUD for tasks', 'Implement task management endpoints', 1, 2, 5),
('Add pagination support', 'Implement pagination for task list endpoints', 0, 1, 5),
('Add sorting logic', 'Allow sorting tasks by priority and creation date', 0, 1, 5),
('Add filtering by status', 'Filter tasks by current status', 0, 1, 5),

-- Project 6
('Create authentication module', 'Implement JWT authentication flow', 1, 2, 6),
('Configure role-based access', 'Add roles and permissions for users', 0, 2, 6),
('Add user registration', 'Implement user registration endpoint', 2, 1, 6),
('Add login endpoint', 'Implement login logic with token generation', 2, 1, 6),
('Secure API endpoints', 'Protect endpoints using authorization policies', 1, 2, 6),
('Implement refresh tokens', 'Add refresh token mechanism', 0, 2, 6),
('Add password hashing', 'Use secure hashing algorithm for passwords', 2, 2, 6),
('Add user profile endpoint', 'Return current user profile data', 1, 1, 6),
('Add logout feature', 'Invalidate refresh tokens on logout', 0, 1, 6),
('Audit authentication flow', 'Review auth flow for security issues', 0, 1, 6),

-- Project 7
('Design task list UI', 'Create UI layout for task list screen', 1, 1, 7),
('Design task details page', 'Create detailed task view UI', 0, 1, 7),
('Add create task form', 'Implement form for creating new tasks', 1, 2, 7),
('Add edit task form', 'Allow editing existing tasks via UI', 0, 2, 7),
('Implement delete confirmation', 'Add confirmation modal before deleting tasks', 0, 1, 7),
('Connect API to frontend', 'Integrate frontend with backend API', 1, 2, 7),
('Handle API errors', 'Show proper error messages in UI', 0, 1, 7),
('Add loading indicators', 'Show loaders during async operations', 0, 0, 7),
('Optimize UI performance', 'Reduce unnecessary re-renders', 0, 0, 7),
('Finalize UI styling', 'Apply final styles and responsive layout', 0, 1, 7),

-- Extra (mixed projects)
('Add logging', 'Integrate structured logging across the system', 1, 1, 5),
('Add global exception handler', 'Handle unhandled exceptions globally', 1, 2, 5),
('Add Swagger documentation', 'Document API endpoints using Swagger', 2, 1, 5),
('Write unit tests', 'Cover business logic with unit tests', 0, 2, 6),
('Add integration tests', 'Test full request pipeline', 0, 2, 6),
('Prepare production config', 'Configure production environment settings', 0, 2, 5),
('Optimize database queries', 'Improve query performance and indexing', 1, 2, 5),
('Add soft delete', 'Implement soft delete for tasks and projects', 0, 1, 5),
('Add search feature', 'Implement search by task title and description', 0, 1, 7),
('Prepare deployment guide', 'Write documentation for deployment process', 0, 1, 6);

-- =========================================
-- Insert into real table
-- =========================================

INSERT INTO TaskItems
(
    Title,
    Description,
    Status,
    CreatedAt,
    UpdatedAt,
    ProjectId,
    Priority
)
SELECT
    Title,
    Description,
    Status,
    DATEADD(
        MINUTE,
        -ABS(CHECKSUM(NEWID())) % 4320, -- last 3 days, realistic time
        SYSDATETIME()
    ) AS CreatedAt,
    NULL AS UpdatedAt,
    ProjectId,
    Priority
FROM @Tasks;
