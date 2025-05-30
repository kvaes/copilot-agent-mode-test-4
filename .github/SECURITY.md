# Security Policy

## Supported Versions

We are committed to addressing security vulnerabilities in the following versions of our project:

| Version | Supported          |
| ------- | ------------------ |
| Latest  | :white_check_mark: |
| < Latest| :x:                |

## Reporting a Vulnerability

We take security seriously. If you discover a security vulnerability, please follow these steps:

### For Public Issues
- Create an issue in our GitHub repository
- Use the "Security" label
- Provide a clear description of the vulnerability

### For Sensitive Security Issues
- Email us directly at security@example.com
- Include "SECURITY" in the subject line
- Provide detailed information about the vulnerability

## What to Include in Your Report

When reporting a security vulnerability, please include:

1. **Description** - A clear description of the vulnerability
2. **Location** - Where the vulnerability exists (file, function, line number)
3. **Impact** - What could an attacker accomplish by exploiting this vulnerability
4. **Steps to Reproduce** - Detailed steps to reproduce the vulnerability
5. **Proof of Concept** - If possible, include a proof of concept
6. **Suggested Fix** - If you have suggestions for fixing the issue

## Security Best Practices

This project follows these security practices:

### Backend Security
- Input validation and sanitization
- Secure API endpoints with proper authentication
- Regular dependency updates via Dependabot
- Container security scanning
- Secure configuration management

### Frontend Security
- XSS protection through proper output encoding
- CSRF protection
- Secure cookie handling
- Content Security Policy (CSP) headers
- Regular dependency updates

### Infrastructure Security
- Container image scanning
- Secrets management
- Network security
- Access control and permissions
- Regular security audits

## Response Timeline

- **Initial Response**: Within 48 hours of receiving a vulnerability report
- **Assessment**: Within 7 days for initial assessment
- **Resolution**: Security fixes are prioritized and typically resolved within 30 days
- **Disclosure**: Public disclosure after the fix is deployed and users have had time to update

## Security Updates

Security updates will be:
- Documented in release notes
- Tagged with appropriate severity levels
- Communicated through our standard channels

## Scope

This security policy applies to:
- Backend C# Azure Functions
- Frontend Vue.js application
- CI/CD pipelines
- Container images
- Configuration files

## Out of Scope

The following are typically out of scope:
- Denial of service attacks
- Issues in dependencies that don't affect our application
- Social engineering attacks
- Physical attacks

## Recognition

We appreciate security researchers who help improve our security. With your permission, we'll acknowledge your contribution in our release notes (unless you prefer to remain anonymous).

## Contact

For security-related questions or concerns:
- Email: security@example.com
- GitHub Security Advisories: Use the "Report a vulnerability" feature in our repository

---

Thank you for helping keep our project and users safe!