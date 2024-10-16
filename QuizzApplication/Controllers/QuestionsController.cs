﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizzApplication.Data;
using QuizzApplication.Models;

namespace QuizzApplication.Controllers
{

    //Warszawa@Warszawa.eu Warszawa2024!
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SubmitQuiz(Dictionary<int, string>UserAnswers)
        {

            if (UserAnswers == null || !UserAnswers.Any())
            {
                return RedirectToAction("Index", "Home");
            }

            var questions = _context.Question.ToList();

            int correctAnswers = 0;
            int totalQuestions = questions.Count;

            foreach (var question in questions)
            {
                if (UserAnswers.ContainsKey(question.Id))
                {
                    var userAnswer = UserAnswers[question.Id];
                    var correctAnswer = question.AnswerText;

                    if (userAnswer == correctAnswer)
                    {
                        correctAnswers++;
                    }
                }
            }


            return RedirectToAction("Results", new { correctAnswers = correctAnswers, totalQuestions = totalQuestions });
        }


        public IActionResult Results(int correctAnswers, int totalQuestions)
        {
            ViewBag.CorrectAnswers = correctAnswers;
            ViewBag.TotalQuestions = totalQuestions;

            return View();
        }


        // GET: Questions
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Question.ToListAsync());
        }

        // GET: Questions/StartQuiz
        public async Task<IActionResult> StartQuiz()
        {
            return View(await _context.Question.ToListAsync());
        }

        // GET: Questions/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,QuestionText,AnswerText")] Question question)
        {
            if (ModelState.IsValid)
            {

                question.CreatedBy = User.Identity.Name; 
                question.CreatedAt = DateTime.UtcNow;

                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 
          
            return View(question);
        }

        // GET: Questions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuestionText,AnswerText")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var oldQuestion = await _context.Question.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);

                    question.CreatedBy = oldQuestion.CreatedBy;
                    question.CreatedAt = oldQuestion.CreatedAt;
                    question.ModifiedBy = User.Identity.Name;
                    question.ModifiedAt = DateTime.UtcNow;
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Question.FindAsync(id);
            if (question != null)
            {
                _context.Question.Remove(question);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.Id == id);
        }
    }
}
